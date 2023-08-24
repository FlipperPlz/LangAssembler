﻿using System;
using System.IO;
using System.Text;
using LangAssembler.Extensions;
using LangAssembler.Internal;
using LangAssembler.Steppers.Options;
using Microsoft.Extensions.Logging;

namespace LangAssembler.Steppers;

public class StringStepper : LaLoggable<IStringStepper>, IStringStepper
{
    /// <summary>
    /// This is the content of the stepper.
    /// </summary>
    /// <remarks>Sometimes referred to as the buffer.</remarks>
    public string Content { get; protected set; }

    /// <summary>
    /// This is the length of the content loaded.
    /// </summary>
    public int Length => Content.Length - 1;

    /// <summary>
    /// This is the current position of the sliding window within the content buffer.
    /// </summary>
    public int Position { get; private set; } = -1;

    /// <summary>
    /// Gets the current character in the content at the Position. If this is null there are one of three reasons:
    /// 1. Our position is before the buffer. (BOF)
    /// 2. Our position is after the buffer. (EOF)
    /// 3. Our buffer is empty, what are you doing! D:
    /// </summary>
    public char? CurrentChar { get; private set; }

    /// <summary>
    /// Gets the character in the content just before the Position. Nullability is used the same way as its used in
    /// <see cref="IStringStepper.CurrentChar"/>.
    /// </summary>
    public char? PreviousChar { get; private set; }
    
    private bool _disposed;

    
    public StringStepper(string content, ILogger<IStringStepper>? logger) : base(logger)
    {
        Content = content;
    }
    
    public StringStepper(BinaryReader reader, Encoding encoding, StepperDisposalOption option, ILogger<IStringStepper>? logger = default, int? length = null, long? stringStart = null) : base(logger)
    {
        var backupStart = reader.BaseStream.Position;
        var start = backupStart;
        if (stringStart is {  } st)
        {
            start = st;
            reader.BaseStream.Seek(st, SeekOrigin.Begin);
        }
        var byteCount = length ?? (int)(reader.BaseStream.Length - reader.BaseStream.Position);

        Content = encoding.GetString(reader.ReadBytes(byteCount));
        switch (option)
        {
            case StepperDisposalOption.JumpBackToStart:
                reader.BaseStream.Seek(backupStart, SeekOrigin.Begin);
                break;
            case StepperDisposalOption.JumpToStringStart:
                reader.BaseStream.Seek(start, SeekOrigin.Begin);
                break;
            case StepperDisposalOption.Dispose:
                reader.Dispose();
                break;
            case StepperDisposalOption.JumpToStringEnd:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(option), option, null);
        }
    }

    ~StringStepper() => Dispose(false);

    /// <summary>
    /// Jumps to a certain position and correctly sets <see cref="IStringStepper.CurrentChar"/> and <see cref="IStringStepper.PreviousChar"/>.
    /// </summary>
    /// <param name="position">The position to jump to.</param>
    public virtual char? JumpTo(int position)
    {
        Position = position;
        PreviousChar = Content.GetOrNull(position - 1);
        return CurrentChar = Content.GetOrNull(position);
    }

    /// <summary>
    /// Resets the contents of the stepper and resets the window.
    /// </summary>
    /// <param name="content">The new content to write to buffer.</param>
    public virtual void ResetStepper(string? content = null)
    {
        if (content is not null)
        {
            Content = content;
        }

        Position = -1;
        PreviousChar = null;
        CurrentChar = null;
    }
    
    protected virtual void Dispose(bool disposing)
    {
        // Check if this object has been disposed already.
        if (_disposed)
        {
            return;
        }

        if (!disposing) return;
        Content = string.Empty;
        _disposed = true;
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}
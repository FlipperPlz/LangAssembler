﻿using System.ComponentModel.DataAnnotations;

namespace LangAssembler.Models.Buffer;

public interface ISlidingBuffer
{
    public long Position { get; }
    public long Length { get; }
    public virtual bool CanEdit => false;
    
    public byte? CurrentByte { get; }
    public byte? PreviousByte { get; }

    public byte? JumpTo(long position);
    public byte? MoveForward([Range(0, int.MaxValue)] int count = 1);
    public byte? MoveBackward([Range(0, int.MaxValue)] int count = 1);
    public byte? PeekNext();

    public Span<byte> PeekAt(long location, [Range(0, int.MaxValue)] long length);

    public void ResetBuffer();

    public Stream AsStream();
    public Span<byte> AsSpan();
}
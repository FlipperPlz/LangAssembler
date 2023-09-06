namespace LangAssembler.Models.Buffer.Encoded;

public static class EncodedSlidingBufferExtensions
{
    public static string PeekForward(this IEncodedSlidingBuffer buffer, int length)
    {
        var position = buffer.Position;
        try
        {
            return NextCharacters(buffer, length);
        }
        finally
        {
            buffer.JumpTo(position);
        }
    }
    
    public static string PeekBackward(this IEncodedSlidingBuffer buffer, int length)
    {
        var position = buffer.Position;
        try
        {
            return PreviousCharacters(buffer, length);
        }
        finally
        {
            buffer.JumpTo(position);
        }
    }
    
    public static string NextCharacters(this IEncodedSlidingBuffer buffer, int length)
    {
        if (length == 0)
            return string.Empty;
    
        var encoding = buffer.Encoding;
        Span<byte> bufferSpan = stackalloc byte[length];
        var actualLength = 0;

        for(var i = 0; i < length; i++)
        {
            var current = buffer.NextCharacter();
            if (!current.HasValue) break;
                
            bufferSpan[actualLength++] = (byte)current.Value;
        }

        return encoding.GetString(bufferSpan[..actualLength]);
    }
    
    public static string PreviousCharacters(this IEncodedSlidingBuffer buffer, int length)
    {
        if (length == 0)
            return string.Empty;
    
        var encoding = buffer.Encoding;
        Span<byte> bufferSpan = stackalloc byte[length];
        var actualLength = 0;

        for(var i = 0; i < length; i++)
        {
            var current = buffer.PreviousCharacter();
            if (!current.HasValue) break;
           
            bufferSpan[actualLength++] = (byte)current.Value;
        }

        bufferSpan[..actualLength].Reverse();

        return encoding.GetString(bufferSpan[..actualLength]);
    }
    
    public static int? NextCharacter(this IEncodedSlidingBuffer buffer)
    {
       var encoding = buffer.Encoding;
       Span<char> chars = stackalloc char[1];
       Span<byte> bytes = stackalloc byte[encoding.IsSingleByte ? 1 : 4];

       for (var i = 0; ; i++)
       {
           var current = buffer.MoveForward();
           if (!current.HasValue)
               return null;

           bytes[i] = current.Value;

           if (encoding.GetCharCount(bytes[..(i + 1)]) <= 0) continue;
           
           encoding.GetChars(bytes[..(i+1)], chars);
           return chars[0];
       }
    }
    
    public static int? PreviousCharacter(this IEncodedSlidingBuffer buffer)
    {
        var encoding = buffer.Encoding;
        Span<char> chars = stackalloc char[1];
        Span<byte> bytes = stackalloc byte[encoding.IsSingleByte ? 1 : 4];

        for (var i = 0; ; i++)
        {
            var current = buffer.MoveBackward();
            if (!current.HasValue)
                return null;

            var pos = bytes.Length - i - 1;
            bytes[pos] = current.Value;

            if (encoding.GetCharCount(bytes[pos..]) <= 0) continue;
           
            encoding.GetChars(bytes[pos..], chars);
            return chars[0];
        }
    }
}
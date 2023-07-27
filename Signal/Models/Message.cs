using System;
using System.Collections.Generic;

namespace Signal.Models;

public partial class Message
{
    public int Msgid { get; set; }

    public int? Convid { get; set; }

    public int? Author { get; set; }

    public string? Content { get; set; }

    public virtual User? AuthorNavigation { get; set; }

    public virtual Conversation? Conv { get; set; }
}

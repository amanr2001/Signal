using System;
using System.Collections.Generic;

namespace Signal.Models;

public partial class Conversation
{
    public int Convid { get; set; }

    public int? User1 { get; set; }

    public int? User2 { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual User? User1Navigation { get; set; }

    public virtual User? User2Navigation { get; set; }
}

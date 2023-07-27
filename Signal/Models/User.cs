using System;
using System.Collections.Generic;

namespace Signal.Models;

public partial class User
{
    public int Userid { get; set; }

    public string? Username { get; set; }

    public string? Userpass { get; set; }

    public virtual ICollection<Conversation> ConversationUser1Navigations { get; set; } = new List<Conversation>();

    public virtual ICollection<Conversation> ConversationUser2Navigations { get; set; } = new List<Conversation>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}

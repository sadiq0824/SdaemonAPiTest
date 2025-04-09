using System;
using System.Collections.Generic;

namespace SdaemonAPiTest.Models;

public partial class ToDo
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public bool IsComplete { get; set; }
}

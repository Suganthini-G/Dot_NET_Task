﻿namespace Dot_NET_Task.Model.DTO
{
    public class QuestionModel
    {
            public string Type { get; set; }
            public string Question { get; set; }
            public List<string> ? Choices { get; set; }
    }
}

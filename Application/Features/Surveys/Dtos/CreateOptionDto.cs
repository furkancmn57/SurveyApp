﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Surveys.Dtos
{
    public class CreateOptionDto
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
    }
}
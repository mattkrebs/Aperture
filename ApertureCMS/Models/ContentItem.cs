﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApertureCMS.Models
{
    public class ContentItem
    {
        public Guid Id { get; set; }
        public string Data { get; set; }
        public string Name { get; set; }
        public  int ContentTypeId { get; set; }
        public ContentType ContentType { get; set; }
    }
}

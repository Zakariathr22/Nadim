﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.Models
{
    public class Office
    {
        public string naming { get; set; }
        public string accreditation { get; set; }
        public string wilaya { get; set; }
        public string municipality { get; set; }
        public string headquarters { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public DateTimeOffset? createdAt { get; set; }
        public DateTimeOffset lastUpdate { get; set; }
        public bool isDeleted { get; set; }
        public DateTimeOffset deletedAt { get; set; }
        public string loger { get; set; }
        public Office office { get; set; }
        public bool? isCompany { get; set; }

        public void clear()
        {
            naming = null;
            accreditation = null;
            wilaya = null;
            municipality = null;
            headquarters = null;
            phone1 = null;
            phone2 = null;
            fax = null;
            email = null;
            createdAt = null;
            isCompany = null;
        }
    }

}
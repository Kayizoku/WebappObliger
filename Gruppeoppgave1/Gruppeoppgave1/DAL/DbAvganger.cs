using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;
using Microsoft.EntityFrameworkCore;

namespace Gruppeoppgave1.Model
{
    public class Avganger
    {
        [Key]
        public int Id { get; set; }
        public string Avgang { get; set; }
    }
}
﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

[Index(nameof(ClientName), IsUnique = true)]

public class ClientEntity
{
    [Key]
    public string id { get; set; } = null!;
    public string ClientName { get; set; } = null!;

    public virtual ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
}


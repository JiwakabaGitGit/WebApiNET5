using System;
using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
  public class memberopContext : DbContext
  {
        public memberopContext(DbContextOptions options) : base(options)
        {
        }

        public memberopContext() : base()
        {
        }

        public DbSet<Member> Members { get; set; }
  }

  [Keyless]
  public class Member
  {
    public int Id { get; set; }

    [DisplayName("氏名")]
    public string Name { get; set; }

    [DisplayName("メールアドレス")]
    public string Email { get; set; }

    [DisplayName("生年月日")]
    //public DateTime Birth { get; set; }
    public string Birth { get; set; }

    [DisplayName("既婚")]
    public bool Married { get; set; }

    [DisplayName("自己紹介")]
    public string Memo { get; set; }
  }
}

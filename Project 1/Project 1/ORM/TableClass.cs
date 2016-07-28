using System;
using System.Data;
using System.IO;
using SQLite;


namespace Project_1.ORM
{
    [Table("TableClasses")]
   public class TableClass
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int Id { get; set; }

        [MaxLength(10)]
        public string UnitCode { get; set; }

        [MaxLength(10)]
        public string Room { get; set; }

        [MaxLength(10)]
        public string Time { get; set; }

        [MaxLength(10)]
        public string Day { get; set; }

        [MaxLength(10)]
        public string Type { get; set; }

    }
}
using System;

using Android.Widget;

using System.Data;
using System.IO;
using SQLite;
using System.Collections.Generic;

namespace Project_1.ORM
{
    class DBRepository
    {

        // Create DB
        public string CreateDB()
        {
            var output = "";
            output += "Creating Database if it doesnt exist.";
            string dbPath = Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.Personal), "classtimetable.db3");

            var db = new SQLiteConnection(dbPath);
            output += "\nDatabase Created...";
            return output;

        }
        // Create table
        public string CreateTable()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.Personal), "classtimetable.db3");

                var db = new SQLiteConnection(dbPath);

                db.CreateTable<TableClass>();
                string result = "Table Created successfully..";
                return result;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        // Insert Record
        public string InsertRecord(string unitCode,string type, string room, string time, string day)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath
                                (Environment.SpecialFolder.Personal), "classtimetable.db3");

                var db = new SQLiteConnection(dbPath);
                TableClass item = new TableClass();
                item.UnitCode = unitCode;
                item.Room = room;
                item.Time = time;
                item.Day = day;
                item.Type = type;
                db.Insert(item);
                return "Record Added...";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        // Retrieve all Records as List
        public List<ClassList> AllClassList()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath
                   (Environment.SpecialFolder.Personal), "classtimetable.db3");

            var db = new SQLiteConnection(dbPath);
            List<ClassList> mitems = new List<ClassList>();
            var table = db.Table<TableClass>();

            foreach (var item in table)
            {
                mitems.Add(new ClassList() { UnitCode = item.UnitCode, Type = item.Type, Room = item.Room, Time = item.Time, Day = item.Day});
            }

            return mitems;

        }
        // Retrieve All Records
        public string GetAllRecords()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath
                   (Environment.SpecialFolder.Personal), "classtimetable.db3");

            var db = new SQLiteConnection(dbPath);

            string output = "";
            output += "Retriveing the data using ORM...";

            var table = db.Table<TableClass>();
          
            foreach (var item in table)
            {
                output += "\n" + item.Id + "," + item.UnitCode + "," + item.Type  + "," + item.Room + "," + item.Time + "," + item.Day;  
            }
         

            return output;
        }

        // Retrieve specific record using ORM
        public string GetTaskById(int id)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath
              (Environment.SpecialFolder.Personal), "classtimetable.db3");

                var db = new SQLiteConnection(dbPath);

                var item = db.Get<TableClass>(id);
                return item.UnitCode + " " + item.Room;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        // Update the record using ORM
        public string updaterecord(int id, string task)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath
           (Environment.SpecialFolder.Personal), "classtimetable.db3");

            var db = new SQLiteConnection(dbPath);

            var item = db.Get<TableClass>(id);
            item.UnitCode = task;
            db.Update(item);
            return "Record Updated...";

        }

    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace My_agenda
{
    class conexion
    {
        public static SQLiteConnection conectar()
        {
            string database = Application.StartupPath + "\\my_agenda.db";
            SQLiteConnection cn = new SQLiteConnection("Data source = " + database);
            return cn;
        }
    }
}

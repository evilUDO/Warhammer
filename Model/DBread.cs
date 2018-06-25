using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Collections.ObjectModel;

namespace Model
{
    class DBread
    {
        private OleDbConnection con;
        private OleDbCommand cmd;
        private Geschoepf g;

        public DBread(ObservableCollection<Geschoepf> charaktere, ObservableCollection<Geschoepf> gegner)
        {
            ErstelleConnection();
            FuelleListCharakter(charaktere);
            FuelleListGegner(gegner);
            con.Close();
        }

        private void ErstelleConnection()
        {
            con = new OleDbConnection(Settings1.Default.DBcon);
            con.Open();
        }

        private void FuelleListGegner(ObservableCollection<Geschoepf> gegner)
        {
            cmd = new OleDbCommand("select * from Monster", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                g = new Geschoepf();
                g = MkGeschoepf(reader);
                gegner.Add(g);
            }
            reader.Close();
        }

        private void FuelleListCharakter(ObservableCollection<Geschoepf> charaktere)
        {
            cmd = new OleDbCommand("select * from Charakter", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                g = new Geschoepf();
                g = MkGeschoepf(reader);
                charaktere.Add(g);
            }
            reader.Close();
        }

        private Geschoepf MkGeschoepf(OleDbDataReader reader)
        {
            int i = 1;
            g.Name = reader.GetString(i++);
            g.Kampfgeschick = reader.GetInt32(i++);
            g.Ballistischefertigkeiten = reader.GetInt32(i++);
            g.Staerke = reader.GetInt32(i++);
            g.Widerstand = reader.GetInt32(i++);
            g.Lebenspunkte = reader.GetInt32(i++);
            g.Initiative = reader.GetInt32(i++);
            g.Aktion = reader.GetInt32(i++);
            g.Waffenbonus = reader.GetInt32(i++);
            g.Ruestungsbonus = reader.GetInt32(i);
            g.AktuelleLP = g.Lebenspunkte;

            return g;
        }


    }
}

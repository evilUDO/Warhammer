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
                g = mkGeschoepf(reader);
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
                g = mkGeschoepf(reader);
                charaktere.Add(g);
            }
            reader.Close();
        }

        private Geschoepf mkGeschoepf(OleDbDataReader reader)
        {
            g.Name = reader.GetString(0);
            g.Kampfgeschick = reader.GetInt32(1);
            g.Ballistischefertigkeiten = reader.GetInt32(2);
            g.Staerke = reader.GetInt32(3);
            g.Widerstand = reader.GetInt32(4);
            g.Lebenspunkte = reader.GetInt32(5);
            g.Initiative = reader.GetInt32(6);
            g.Aktion = reader.GetInt32(7);
            g.Waffenbonus = reader.GetInt32(8);
            g.Ruestungsbonus = reader.GetInt32(9);

            return g;
        }
    }
}

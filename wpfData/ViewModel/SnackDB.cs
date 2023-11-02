using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using wpfData.Model;
using wpfData_Step_4.Model;
using wpfData_Step_4.ViewModel;
using static wpfData.Model.Snack;

namespace wpfData.ViewModel
{
    internal class SnackDB:BaseDB
    {
        protected override BaseEntity NewEntity()
        {
            return new Snack() as BaseEntity;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Snack snack = (Snack)entity;
            snack.snackid = int.Parse(reader["SnackID"].ToString());
            snack.snackName = reader["SnackName"].ToString();
            snack.Calories = int.Parse(reader["Calories"].ToString());
          
            snack.Issalty = bool.Parse(reader["IsSalty"].ToString());
          

            return snack;
        }
        public SnackList SelectAll()
        {
            this.command.CommandText = "SELECT * FROM TblSnacks";
            SnackList list = new SnackList(base.ExecuteCommand());
            return list;
        }

        public Snack SelectById(int id)
        {
            command.CommandText = $"SELECT * FROM TblSnacks WHERE (ID = {id})";
            SnackList list = new SnackList(base.ExecuteCommand());
            if (list.Count == 1)
                return list[0];
            return null;
        }
        public SnackList SelectByUser(User user)
        {
            command.CommandText = "Select *, TblUsersSnacks.snackID as [aa] " + 
                " FROM (TblUsersSnacks INNER JOIN TblSnacks " +
                " ON TblUsersSnacks.snackID = TblSnacks.SnackID) " + $" WHERE userID={user.Id}";
            SnackList list = new SnackList(base.ExecuteCommand());
            return list;

        }
    }
}

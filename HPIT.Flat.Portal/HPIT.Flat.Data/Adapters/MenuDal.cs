using HPIT.Flat.Data.Entitys;
using HPIT.Flat.Data.ExtEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Flat.Data.Adapter
{
    public class MenuDal
    {
        public static MenuDal Instance = new MenuDal();

        public FlatContext context { get; set; }

        public MenuDal() {
            this.context = new FlatContext();
        }

        public List<MenuExt> GetMenusByRoleName(string roleName)
        {
            List<MenuExt> meuns = new List<MenuExt>();
            string sql = string.Format(@" select * from ( select m.*,r.RoleName,r.RoleID from RoleMenus rm 
                                          left join Menus m on rm.MenuID = m.MenuID 
                                          left join Roles r on rm.RoleID = r.RoleID) as t where t.RoleName = '{0}'", roleName);
            using (var context = new FlatContext())
            {
                meuns = context.Database.SqlQuery<MenuExt>(sql).ToList();
            }
            List<MenuExt> first = meuns.Where(r => r.MenuType == "一级菜单").ToList();
            List<MenuExt> second = meuns.Where(r => r.MenuType == "二级菜单").ToList();
            foreach (MenuExt menu in first)
            {
                foreach (MenuExt item in second)
                {
                    if (item.ParentID == menu.MenuID)
                    {
                        menu.Children.Add(item);
                    }
                }
            }
            return first;
        }
    }
}

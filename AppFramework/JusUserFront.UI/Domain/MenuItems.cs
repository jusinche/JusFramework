using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Antlr.Runtime.Misc;
using JusNucleo.Bl.Sistema.Menu;
using JusUserFront.UI.Models;

namespace JusUserFront.UI.Domain
{
    public class MenuItems
    {
        public IEnumerable<MenuNavBar> GetMenuItems(string usuario)
        {
            var criteria =ModuloFuncionalidadCriteria.New();
            criteria.Usuario = usuario;
            var lista=ModuloFuncionalidadList.Get(criteria);

            var menu = new List<MenuNavBar>();
            var padres = lista.Select(l => new { l.ModuloId, l.ModuloMenu, l.ModuloNombre }).Distinct();
            foreach (var padre in padres)
            {
                menu.Add(new MenuNavBar { Id = padre.ModuloId, NameOption = padre.ModuloMenu, ImageClass = "fa fa-sitemap fa-fw", Status = true, IsParent = true, ParentId = 0 });
            }
            foreach (var item in lista)
            {
                menu.Add(new MenuNavBar { Id = item.Id, NameOption = item.FuncionalidadMenu, Controller = item.Controlador, Action = item.Accion, Status = true, IsParent = false, ParentId = item.ModuloId });
            }
            
           /* menu.Add(new MenuNavBar { Id = 2, NameOption = "Charts", ImageClass = "fa fa-sitemap fa-fw", Status = true, IsParent = true, ParentId = 0 });
            menu.Add(new MenuNavBar { Id = 3, NameOption = "Flot Charts", Controller = "Home", Action = "FlotCharts", Status = true, IsParent = false, ParentId = 2 });
            menu.Add(new MenuNavBar { Id = 4, NameOption = "Morris.js Charts", Controller = "Home", Action = "MorrisCharts", Status = true, IsParent = false, ParentId = 2 });
            menu.Add(new MenuNavBar { Id = 7, NameOption = "UI Elements", ImageClass = "fa fa-sitemap fa-fw", Status = true, IsParent = true, ParentId = 0 });
            menu.Add(new MenuNavBar { Id = 8, NameOption = "Panels and Wells", Controller = "Home", Action = "Panels", Status = true, IsParent = false, ParentId = 7 });
            menu.Add(new MenuNavBar { Id = 9, NameOption = "Buttons", Controller = "Home", Action = "Buttons", Status = true, IsParent = false, ParentId = 7 });
            menu.Add(new MenuNavBar { Id = 10, NameOption = "Notifications", Controller = "Home", Action = "Notifications", Status = true, IsParent = false, ParentId = 7 });
            menu.Add(new MenuNavBar { Id = 11, NameOption = "Typography", Controller = "Home", Action = "Typography", Status = true, IsParent = false, ParentId = 7 });
            menu.Add(new MenuNavBar { Id = 12, NameOption = "Icons", Controller = "Home", Action = "Icons", Status = true, IsParent = false, ParentId = 7 });
            menu.Add(new MenuNavBar { Id = 13, NameOption = "Grid", Controller = "Home", Action = "Grid", Status = true, IsParent = false, ParentId = 7 });
            menu.Add(new MenuNavBar { Id = 14, NameOption = "Multi-Level Dropdown", ImageClass = "fa fa-sitemap fa-fw", Status = true, IsParent = true, ParentId = 0 });
            menu.Add(new MenuNavBar { Id = 15, NameOption = "Second Level Item", Status = true, IsParent = false, ParentId = 14 });
            menu.Add(new MenuNavBar { Id = 16, NameOption = "Sample Pages", ImageClass = "fa fa-sitemap fa-fw", Status = true, IsParent = true, ParentId = 0 });
            menu.Add(new MenuNavBar { Id = 17, NameOption = "Blank Page", Controller = "Home", Action = "Blank", Status = true, IsParent = false, ParentId = 16 });
            menu.Add(new MenuNavBar { Id = 18, NameOption = "Login Page", Controller = "Home", Action = "Login", Status = true, IsParent = false, ParentId = 16 });
            */
            return menu.ToList();
        }
    }
}
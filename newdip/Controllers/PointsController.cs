using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

using newdip.Models;

namespace newdip.Controllers
{
    public class PointsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public class P 
        {
            public string firstx { get; set; }
            public string firsty { get; set; }
            public string level { get; set; }
            public string id { get; set; }
        }
        public class Segment
        {
            public string firstx { get; set; }
            public string firsty { get; set; }
            public string secondx { get; set; }
            public string secondy { get; set; }
            public string level { get; set; }
            public string id { get; set; }
            public bool isWaypoint { get; set; }
        }

        public class Rectangle
        {
            public string firstx { get; set; }
            public string firsty { get; set; }
            public string secondx { get; set; }
            public string secondy { get; set; }
            public string thirdx { get; set; }
            public string thirdy { get; set; }
            public string fourthx { get; set; }
            public string fourthy { get; set; }
            public string level { get; set; }
            public string id { get; set; }
        }
        public PointM Similar(int x, int y,Floor floor, bool isway)
        {
            if (!isway)
            {
                var points = db.Points.Where(xx => xx.FloorId == floor.FloorId&&!xx.IsWaypoint).ToList();
                foreach (var element in points)
                {
                    for (int i = -5; i < 6; i++)
                        for (int j = -5; j < 6; j++)
                        {
                            if (element.X + i == x && element.Y + j == y) return element;
                        }
                }

                return null;
            }
            else 
            {
                var points = db.Points.Where(xx => xx.FloorId == floor.FloorId&&xx.IsWaypoint).ToList();
                foreach (var element in points)
                {
                    for (int i = -5; i < 6; i++)
                        for (int j = -5; j < 6; j++)
                        {
                            if (element.X + i == x && element.Y + j == y && element.IsWaypoint) return element;
                        }
                }

                return null;
            }
        }
        public int Etazh(string income)
        {
            return Convert.ToInt32(income.Substring(0, income.Length - 5));
        }
        
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public Room Room([FromBody] P point)
        {
           

            int id = Convert.ToInt32(point.id);
            int level = Etazh(point.level);
            EdgeM edge = new EdgeM();
            List<PointM> points = db.Floors.Where(x => x.Level == level && x.BuildingId == id).Include(x => x.Points).FirstOrDefault().Points.ToList();//этаж просмотр
            ///создание и добавление первой точки
            Room result = new Room();
            foreach(var element in points)
            { for (int i = -2; i < 3; i++)
                for (int j = -2; j < 3; j++)
                {
                        if (element.IsWaypoint  && element.X == Convert.ToInt32(point.firstx)+i && element.Y  == Convert.ToInt32(point.firsty)+j) //если нашли такую точку на этаже
                        {
                            var room = db.Rooms.Include(x => x.Points).ToList();
                            foreach (var vroom in room) //ищем комнату
                            {
                                for (int k = 0; k < vroom.Points.Count; k++) 
                                {
                                    if (vroom.Points[k].IsWaypoint && 
                                        vroom.Points[k].X == element.X && 
                                        vroom.Points[k].Y == element.Y) 
                                    {
                                    result = new Room(vroom.Name,vroom.Description,vroom.Timetable,vroom.Phone,vroom.Site);
                                    }
                                }
                            }
                            
                        }
                } 
            }
                  
                    return result;
           
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public void Stair([FromBody] P point)
        {
            int id = Convert.ToInt32(point.id);
            int level = Etazh(point.level);
            var floor = db.Floors.FirstOrDefault(x => x.BuildingId == id && x.Level == level);
            var upfloor = db.Floors.FirstOrDefault(x => x.BuildingId == id && x.Level == level+1);
            var downfloor = db.Floors.FirstOrDefault(x => x.BuildingId == id && x.Level == level-1);
            List<PointM> points = db.Points.Where(x => x.FloorId == floor.FloorId && x.IsWaypoint).ToList();
            List<PointM> uppoints = db.Points.Where(x => x.FloorId == upfloor.FloorId && x.IsWaypoint).ToList();
            List<PointM> downpoints = db.Points.Where(x => x.FloorId == downfloor.FloorId && x.IsWaypoint).ToList();
            PointM newpoint = null;
            PointM uppoint = null;
            PointM downpoint = null;
            ///создание и добавление первой точки
            newpoint=Similar(Convert.ToInt32(point.firstx), Convert.ToInt32(point.firsty), floor, true);
            if (newpoint == null) 
            {
                newpoint = new PointM();
                newpoint.X = Convert.ToInt32(point.firstx);
                newpoint.Y = Convert.ToInt32(point.firsty);
                newpoint.IsWaypoint = true;
                newpoint.FloorId = floor.FloorId;
                db.Points.Add(newpoint);
                db.SaveChanges();
                newpoint = db.Points.ToList().Last();
            }
            if (upfloor != null)
            { uppoint = Similar(Convert.ToInt32(point.firstx), Convert.ToInt32(point.firsty), upfloor, true);
                if (uppoint == null)
                {
                    uppoint = new PointM();
                    uppoint.X = Convert.ToInt32(point.firstx);
                    uppoint.Y = Convert.ToInt32(point.firsty);
                    uppoint.IsWaypoint = true;
                    uppoint.FloorId = upfloor.FloorId;
                    db.Points.Add(uppoint);
                    db.SaveChanges();
                    uppoint = db.Points.ToList().Last();
                } 
            }
            if (downfloor != null)
            {
                downpoint = Similar(Convert.ToInt32(point.firstx), Convert.ToInt32(point.firsty), downfloor, true);
                if (downpoint == null)
                {
                    downpoint = new PointM();
                    downpoint.X = Convert.ToInt32(point.firstx);
                    downpoint.Y = Convert.ToInt32(point.firsty);
                    downpoint.IsWaypoint = true;
                    downpoint.FloorId = downfloor.FloorId;
                    db.Points.Add(downpoint);
                    db.SaveChanges();
                    downpoint = db.Points.ToList().Last();
                }
            }
            EdgeM Upedge = null;
            Upedge = db.Edges.FirstOrDefault(
                (xx => xx.PointFromId == newpoint.Id && xx.PointToId == uppoint.Id)
                );
            if (Upedge == null)
            {
                Upedge = db.Edges.FirstOrDefault(xx => xx.PointToId == uppoint.Id && xx.PointFromId == newpoint.Id);
            }
            if (Upedge == null)
            {
                Upedge = new EdgeM(13, newpoint.Id, uppoint.Id);
                db.Edges.Add(Upedge);
                db.SaveChanges();
            }
            EdgeM Downedge = null;
            Downedge = db.Edges.FirstOrDefault(
                (xx => xx.PointFromId == newpoint.Id && xx.PointToId == downpoint.Id)
                );
            if (Downedge == null)
            {
                Downedge = db.Edges.FirstOrDefault(xx => xx.PointToId == downpoint.Id && xx.PointFromId == newpoint.Id);
            }
            if (Downedge == null)
            {
                Downedge = new EdgeM(13, newpoint.Id, downpoint.Id);
                db.Edges.Add(Downedge);
                db.SaveChanges();
            }


        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public PointM Move([FromBody] PointM point)
        {
            //var pointslist = db.Points.ToList();
            //var res = Similar(Convert.ToInt32(po.firstx), Convert.ToInt32(po.firsty));
            // Building building = db.Buildings.Where(x => x.Id == k).Include(x => x.Floors).FirstOrDefault();//получаем нужно здание

            int id = Convert.ToInt32(point.Id);
            PointM moved = db.Points.Where(xx=>xx.Id==point.Id).FirstOrDefault();//этаж просмотр
            ///создание и добавление первой точки
            moved.X = point.X;
            moved.Y = point.Y;
            db.SaveChanges();
            PointM checkd = db.Points.Where(xx => xx.Id == point.Id).FirstOrDefault();

            return moved;

        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        public PointM Del([FromBody] PointM point)
        {
            //var pointslist = db.Points.ToList();
            //var res = Similar(Convert.ToInt32(po.firstx), Convert.ToInt32(po.firsty));
            // Building building = db.Buildings.Where(x => x.Id == k).Include(x => x.Floors).FirstOrDefault();//получаем нужно здание

            List<EdgeM> edges = db.Edges.Where(x => x.PointFromId == point.Id || x.PointToId == point.Id).ToList();
            int id = Convert.ToInt32(point.Id);
            for (int i = 0; i < edges.Count; i++) 
            {
                db.Edges.Remove(edges[i]);
                db.SaveChanges();
            }
            PointM moved = db.Points.Where(xx => xx.Id == point.Id).FirstOrDefault();//этаж просмотр
            ///создание и добавление первой точки
            db.Points.Remove(moved);
            db.SaveChanges();
            PointM checkd = db.Points.Where(xx => xx.Id == point.Id).FirstOrDefault();

            return moved;

        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        public void Line([FromBody] Segment po)
        {
            //var pointslist = db.Points.ToList();
            //var res = Similar(Convert.ToInt32(po.firstx), Convert.ToInt32(po.firsty));
            // Building building = db.Buildings.Where(x => x.Id == k).Include(x => x.Floors).FirstOrDefault();//получаем нужно здание
            if (po.secondx != null)
            {
                int id = Convert.ToInt32(po.id);
                int level = Etazh(po.level);
                EdgeM edge = new EdgeM();
                Floor floor1 = db.Floors.Where(x => x.Level == level && x.BuildingId == id).Include(x => x.Points).FirstOrDefault();//этаж просмотр
                Operation(po.secondx, po.secondy, po.firstx, po.firsty, floor1, po.isWaypoint);
                var list = db.Edges.ToList();
            }

        }
        public void Operation(string x1, string y1,string x2,string y2, Floor floor, bool isway) 
        {

            if (!isway)
            {
                PointM point = Similar(Convert.ToInt32(x1), Convert.ToInt32(y1), floor, isway);
                EdgeM edge = new EdgeM();
                //bool isexist = false;
                int X1 = Convert.ToInt32(x1);
                int Y1 = Convert.ToInt32(y1);
                int X2 = Convert.ToInt32(x2);
                int Y2 = Convert.ToInt32(y2);
                ///первая вершина
                if (point == null || point.Id == 0)
                {
                    point = new PointM();
                    point.X = X1;
                    point.Y = Y1;
                    point.IsWaypoint = false;
                    point.FloorId = floor.FloorId;
                    db.Points.Add(point);
                    db.SaveChanges();
                    edge.PointFromId = db.Points.ToList().Last().Id;
                }
                else
                {
                    //point = db.Points.FirstOrDefault(x => x.X == X1 && x.Y == Y1 && x.FloorId == floor.FloorId);
                    edge.PointFromId = point.Id;
                }
                ///2 вершина
                PointM point2 = Similar(Convert.ToInt32(x2), Convert.ToInt32(y2), floor,isway);
                if (point2 == null)
                {
                    point2 = new PointM();
                    point2.X = Convert.ToInt32(x2);
                    point2.Y = Convert.ToInt32(y2);
                    point2.IsWaypoint = false;
                    point2.FloorId = floor.FloorId;
                    db.Points.Add(point2);
                    db.SaveChanges();
                    ///сохранение ребра
                    edge.PointToId = db.Points.ToList().Last().Id;//Id первой вершины
                }
                else
                {
                    //point2 = db.Points.FirstOrDefault(x => x.X == X2 && x.Y == Y2 && x.FloorId == floor.FloorId);
                    edge.PointToId = point2.Id;
                }
                edge.Weight = Math.Sqrt(Math.Pow(point2.X - point.X, 2) +
                    Math.Pow(point2.Y - point.Y, 2)) / 75.9;
                var exedge = db.Edges.FirstOrDefault(
                    (xx => xx.PointFromId == point.Id && xx.PointToId == point2.Id)
                    );
                if (exedge==null) 
                {
                    exedge = db.Edges.FirstOrDefault(xx => xx.PointToId == point.Id && xx.PointFromId == point2.Id);
                }
                if (exedge == null)
                {
                db.Edges.Add(edge);
                db.SaveChanges();
                }
            }
            else 
            {
                PointM point = Similar(Convert.ToInt32(x1), Convert.ToInt32(y1), floor,isway);
                EdgeM edge = new EdgeM();
                //bool isexist = false;
                int X1 = Convert.ToInt32(x1);
                int Y1 = Convert.ToInt32(y1);
                int X2 = Convert.ToInt32(x2);
                int Y2 = Convert.ToInt32(y2);
                ///первая вершина
                if (point == null || point.Id == 0)
                {
                    point = new PointM();
                    point.X = X1;
                    point.Y = Y1;
                    point.IsWaypoint = true;
                    point.FloorId = floor.FloorId;
                    db.Points.Add(point);
                    db.SaveChanges();
                    edge.PointFromId = db.Points.ToList().Last().Id;
                }
                else
                {
                    //point = db.Points.FirstOrDefault(x => x.X == X1 && x.Y == Y1 && x.FloorId == floor.FloorId);
                    edge.PointFromId = point.Id;
                }
                ///2 вершина
                PointM point2 = Similar(Convert.ToInt32(x2), Convert.ToInt32(y2), floor,isway);
                if (point2 == null)
                {
                    point2 = new PointM();
                    point2.X = Convert.ToInt32(x2);
                    point2.Y = Convert.ToInt32(y2);
                    point2.IsWaypoint = true;
                    point2.FloorId = floor.FloorId;
                    db.Points.Add(point2);
                    db.SaveChanges();
                    ///сохранение ребра
                    edge.PointToId = db.Points.ToList().Last().Id;//Id первой вершины
                }
                else
                {
                    //point2 = db.Points.FirstOrDefault(x => x.X == X2 && x.Y == Y2 && x.FloorId == floor.FloorId);
                    edge.PointToId = point2.Id;
                }
                edge.Weight = Math.Sqrt(Math.Pow(point2.X - point.X, 2) +
                    Math.Pow(point2.Y - point.Y, 2)) / 75.9;
                db.Edges.Add(edge);
                db.SaveChanges();
            }

        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public void AddRectangle([FromBody] Rectangle po)
        {
            //var pointslist = db.Points.ToList();
            //var res = Similar(Convert.ToInt32(po.firstx), Convert.ToInt32(po.firsty));
            // Building building = db.Buildings.Where(x => x.Id == k).Include(x => x.Floors).FirstOrDefault();//получаем нужно здание
            int id = Convert.ToInt32(po.id);
            int level = Etazh(po.level);
            //Edge edge = new Edge();
            Floor floor1 = db.Floors.Where(x => x.Level == level && x.BuildingId == id).Include(x => x.Points).Include(x=>x.Rooms).FirstOrDefault();//этаж просмотр
            Operation(po.firstx, po.firsty, po.secondx, po.secondy, floor1,false);
            Operation(po.secondx, po.secondy, po.thirdx, po.thirdy, floor1,false);
            Operation(po.thirdx, po.thirdy, po.fourthx, po.fourthy, floor1,false);
            Operation(po.fourthx, po.fourthy, po.firstx, po.firsty, floor1,false);
            Room room = new Room();

            int px1 = Convert.ToInt32(po.firstx);
            int py1 = Convert.ToInt32(po.firsty);
            int px2 = Convert.ToInt32(po.secondx);
            int py2 = Convert.ToInt32(po.secondy);
            int px3 = Convert.ToInt32(po.thirdx);
            int py3 = Convert.ToInt32(po.thirdy);
            int px4 = Convert.ToInt32(po.fourthx);
            int py4 = Convert.ToInt32(po.fourthy);
            db.Rooms.Add(room);
            db.SaveChanges();
         
            PointM middle = new PointM();
            middle.IsWaypoint = true;
            middle.X = (Convert.ToInt32(po.firstx) + Convert.ToInt32(po.thirdx)) / 2;
            middle.Y = (Convert.ToInt32(po.firsty) + Convert.ToInt32(po.thirdy)) / 2;
            middle.FloorId = floor1.FloorId;
            middle.RoomId = db.Rooms.ToList().Last().RoomId;
            db.Points.Add(middle);
            db.SaveChanges();
            var list = db.Edges.ToList();
        }
    }
}

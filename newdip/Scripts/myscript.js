function updatedraw()
{
    id = document.getElementById("BuildingId").innerHTML;
    x = document.getElementById("Floorlevel").innerHTML;
    let url = "https://localhost:44336/api/Floors/Edges?level=" + x + '&&id=' + id;
    async function getedges() {
        let response = await fetch(url);
        edges = await response.json();
        edges.forEach(function (item, i, edges) {
            if (item.PointFrom.IsWaypoint) context.strokeStyle = 'green';
            else context.strokeStyle = 'black';
            context.beginPath();
            let xx = item.PointFrom.X + cx;
            let yy = cy - item.PointFrom.Y;
            let xxx = item.PointTo.X + cx;
            let yyy = cy - item.PointTo.Y;
            context.moveTo(xx, yy);
            context.lineTo(xxx, yyy);
            context.stroke();
        }
        );
    }
    getedges();
    url = "https://localhost:44336/api/PointMs/FloorPoints?level=" + x + '&&id=' + id;
    async function getpoints() {
        let response = await fetch(url);
        points = await response.json();
        context.clearRect(0, 0, canvas.width, canvas.height);
        points.forEach(function (item, i, points) {
            context.beginPath();
            if (item.IsWaypoint) {
                context.strokeStyle = 'orange';
                context.arc(item.X + cx, cy - item.Y, 3, 0, Math.PI * 2, true); //центр
                context.stroke();
            }
            else {
                context.strokeStyle = 'black';
                context.arc(item.X + cx, cy - item.Y, 5, 0, Math.PI * 2, true); //центр
                context.stroke();
            }

        });
        context.strokeStyle = 'black';
    }
    getpoints();
}
function axis()
{
    contextdraw.closePath();
    contextdraw.beginPath();
    contextdraw.moveTo(cx, cy);//оси
    contextdraw.lineTo(cx, cy - 150);
    contextdraw.lineTo(cx - 5, cy - 140);
    contextdraw.lineTo(cx, cy - 150);
    contextdraw.lineTo(cx + 5, cy - 140);
    contextdraw.lineTo(cx, cy - 150);
    contextdraw.lineTo(cx, cy);
    contextdraw.lineTo(cx + 150, cy);
    contextdraw.lineTo(cx + 140, cy - 5);
    contextdraw.lineTo(cx + 150, cy);
    contextdraw.lineTo(cx + 140, cy + 5);
    contextdraw.lineTo(cx + 150, cy);
    contextdraw.lineTo(cx, cy);
    contextdraw.fillText("Y", cx + 5, cy - 140);
    contextdraw.fillText("X", cx + 135, cy - 5);
    contextdraw.stroke();
    contextdraw.closePath();
}

var points, edges; var cx, cy;
var selectedx, selectedy;
var selectedpoint;
var selectedid, number, pointtodel;//выбранная точка, ее номер в списке, точка на удаление
var delx, dely;//координаты точки удаления
var floor;
let workers;
var clinex,cliney;
let ctrl = false;
let shift = false;
var room;
var canvas; var contextdraw;//temp
var staticpaper; var context;//view
 //переменные
if (window.addEventListener) {
    window.addEventListener('load', function () {
        var tool;
        var tool_default = 'moveimg';
        
        var mdx, mdy;
        var centerx, centery;//показывает центр
        var lvl, id;//этажи, здание идентификаторы

        function init() {
            id = document.getElementById("BuildingId");
            lvl = document.getElementById("spisok");
            //
            canvas = document.getElementById("imageTemp");
            staticpaper = document.getElementById("imageView");
            centerx = document.getElementById("centerx");//центр по х
            centery = document.getElementById("centery");//центр по у
            //
            if (canvas) {
                contextdraw = canvas.getContext('2d');
                context = staticpaper.getContext('2d');
                //--
                contextdraw.beginPath();
                contextdraw.strokeStyle = 'green';
                cx = canvas.width / 2;
                cy = canvas.height / 2;
                clinex = cx;
                cliney = cy;
                axis();
                // contextdraw.arc(cx, cy, 2, 0, Math.PI * 2, true); //центр
                //contextdraw.arc(cx, cy, 400, 0, Math.PI * 2, true); //центр
                //contextdraw.stroke();
                //context.drawImage(canvas, 10, 10);
                //contextdraw.drawImage(canvas, -10, -10);
                //contextdraw.closePath();
                //img_update();
                var toolselect = document.getElementById('dtool');
                toolselect.addEventListener('change', tool_change, false);
                //// Attach the mousedown, mousemove and mouseup event listeners.
                canvas.addEventListener('mousedown', canvasact, false);
                canvas.addEventListener('mousemove', canvasact, false);
                canvas.addEventListener('mouseup', canvasact, false);
            }

            if (tools[tool_default]) {
                tool = new tools[tool_default]();
                toolselect.value = tool_default;
            }



        }
        
        //function img_update() {
        //    //context.clearRect(0, 0, canvas.width, canvas.height);
        //    //context.drawImage(canvas, 0, 0);
        //    //contextdraw.clearRect(0, 0, canvas.width, canvas.height);
        //}
        //действие на канвасе
        function canvasact(ev) {

            if (ev.layerX || ev.layerX == 0) { // Firefox
                ev._x = ev.layerX;
                ev._y = ev.layerY;
                //cx = ev.layerX;
                //cy = ev.layerY;
                centerx.innerHTML = ev._x - cx;
                centery.innerHTML = cy - ev._y;
            }
            else if (ev.offsetX || ev.offsetX == 0) { // Opera
                ev._x = ev.offsetX;
                ev._y = ev.offsetY;
            }
            // Call the event handler of the tool.
            var func = tool[ev.type];
            if (func) {
                func(ev);
            }
        }

            //обработчик смены инструмента
        function tool_change(ev) {
            if (tools[this.value]) {
                tool = new tools[this.value]();
            }
        }
        var tools = {};
        //линия
        tools.line = function () {
            var tool = this;
            tool.started = false;
            tool.perenos = false;
            let beg;
            //нажатие лкм
            this.mousedown = function (ev)
            {
                tool.started = true;
                beg = shift;
                    tool.x0 = ev._x;
                    tool.y0 = ev._y;
            };
            //отрисовка линии за курсором
            this.mousemove = function (ev) {
                if (!tool.started) return;
                if (!shift) {
                    contextdraw.clearRect(0, 0, canvas.width, canvas.height);
                    mdx = tool.x0;
                    mdy = tool.y0;
                    context.closePath();
                    contextdraw.beginPath();
                    contextdraw.strokeStyle = 'black';
                    contextdraw.moveTo(tool.x0, tool.y0);
                    contextdraw.lineTo(ev._x, ev._y);
                    contextdraw.stroke(); contextdraw.closePath();
                    contextdraw.beginPath();
                    contextdraw.moveTo(tool.x0 + 3, tool.y0);
                    contextdraw.arc(tool.x0, tool.y0, 4, 0, Math.PI * 2, true); // Внешняя окружность
                    contextdraw.moveTo(ev._x + 3, ev._y);
                    contextdraw.arc(ev._x, ev._y, 4, 0, Math.PI * 2, true);
                    contextdraw.stroke();
                    contextdraw.closePath();
                }
                else
                {
                    contextdraw.clearRect(0, 0, canvas.width, canvas.height);
                    mdx = tool.x0;
                    mdy = tool.y0;
                    context.closePath();
                    contextdraw.beginPath();
                    contextdraw.strokeStyle = 'green';
                    contextdraw.moveTo(tool.x0, tool.y0);
                    contextdraw.lineTo(ev._x, ev._y);
                    contextdraw.stroke(); contextdraw.closePath();
                    contextdraw.beginPath();
                    contextdraw.moveTo(tool.x0 + 3, tool.y0);
                    contextdraw.arc(tool.x0, tool.y0, 4, 0, Math.PI * 2, true); // Внешняя окружность
                    contextdraw.moveTo(ev._x + 3, ev._y);
                    contextdraw.arc(ev._x, ev._y, 4, 0, Math.PI * 2, true);
                    contextdraw.stroke();
                    contextdraw.closePath();
                }
            };

            this.mouseup = function (ev)
            {
                //прекращение отрисовки и сохранение
                if (tool.started) {
                    tool.mousemove(ev);
                    //contexto.arc(tool.mousemove(ev).x0, tool.mousemove(ev).x0,50, 0, 2 * Math.PI, false)
                    tool.started = false;
                    //img_update();
                }
                //отправка пост запроса на добавление
                if (beg && beg == shift) {
                    $('#imageTemp').mouseup
                    {
                        new function () {

                            $.ajax({
                                url: '/Points/Line',
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: JSON.stringify({
                                    firstx: ev._x - cx, firsty: cy - ev._y, secondx: mdx - cx, secondy: cy - mdy, level: lvl.options[lvl.selectedIndex].text,
                                    id: id.innerHTML, IsWaypoint:true
                                }),
                                complete: function () {
                                }
                            });

                        }
                    }
                }
                else
                {
                    $('#imageTemp').mouseup
                    {
                        new function () {

                            $.ajax({
                                url: '/Points/Line',
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: JSON.stringify({
                                    firstx: ev._x - cx, firsty: cy - ev._y, secondx: mdx - cx, secondy: cy - mdy, level: lvl.options[lvl.selectedIndex].text,
                                    id: id.innerHTML, IsWaypoint:false
                            }),
                                complete: function () {
                                }
                            });

                        }
                    }
                }
                //setTimeout(, 10000);
                    updatedraw()
            };
        };
        //прямоугольник
        tools.rect = function () {
            var tool = this;
            this.started = false;
            //нажатие лкм
            this.mousedown = function (ev) {
                tool.started = true;
                tool.x0 = ev._x;
                tool.y0 = ev._y;
            };
            //отрисовка прямоугольника за курсором
            this.mousemove = function (ev) {
                if (!tool.started) return;
                //значения
                var x = Math.min(ev._x, tool.x0),
                    y = Math.min(ev._y, tool.y0),
                    w = Math.abs(ev._x - tool.x0),
                    h = Math.abs(ev._y - tool.y0);

                if (!w || !h) return;
                //отрисовка прямоугольника
                contextdraw.clearRect(0, 0, canvas.width, canvas.height);
                contextdraw.beginPath();
                contextdraw.strokeStyle = 'grey';
                contextdraw.arc(x, y, 5, 0, Math.PI * 2, true);
                contextdraw.stroke();
                contextdraw.closePath();
                contextdraw.beginPath();
                contextdraw.arc(x + w, y, 5, 0, Math.PI * 2, true);
                contextdraw.stroke();
                contextdraw.closePath(); contextdraw.beginPath();
                contextdraw.arc(x, y + h, 5, 0, Math.PI * 2, true);
                contextdraw.stroke();
                contextdraw.closePath(); contextdraw.beginPath();
                contextdraw.arc(x + w, h + y, 5, 0, Math.PI * 2, true);
                contextdraw.stroke();
                contextdraw.closePath();
                contextdraw.beginPath();
                contextdraw.strokeRect(x, y, w, h);
            };
            this.mouseup = function (ev) {
                if (tool.started) {
                    tool.mousemove(ev);
                    //contexto.arc(tool.mousemove(ev).x0, tool.mousemove(ev).x0,50, 0, 2 * Math.PI, false)
                    tool.started = false;
                    mdx = tool.x0;
                    mdy = tool.y0;
                    //отпарвка пост запроса на добавление прямоугольника
                    $('#imageTemp').mouseup
                    {
                        new function () {
                            $.ajax({
                                url: '/Points/AddRectangle',
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: JSON.stringify({
                                    firstx: ev._x - cx, firsty: cy - ev._y, secondx: mdx - cx, secondy: cy - ev._y,
                                    thirdx: mdx - cx, thirdy: cy - mdy, fourthx: ev._x - cx, fourthy: cy - mdy,
                                    level: lvl.options[lvl.selectedIndex].text,
                                    id: id.innerHTML
                                }),
                                success: function () { }
                            });

                        }

                    }
                }
                updatedraw

            };

        };
        //информация о комнате
        tools.info = function () {
            var tool = this;
            this.started = false;

            this.mousedown = function (ev) {
                tool.started = true;
                tool.x0 = ev._x;
                tool.y0 = ev._y;
                $('#imageTemp').mouseup
                {
                    let x = document.getElementById("Floorlevel").innerHTML;
                    let id = document.getElementById("BuildingId").innerHTML;
                    let url = "https://localhost:44336/api/Rooms/Room?level=" + x + '&&id=' + id + '&&x=' + (ev._x - cx) + '&&y=' + (cy - ev._y);
                    //console.log(url);
                    //метод поиска комнаты
                    async function GetRooms() {
                        let response = await fetch(url);
                        console.log(response);
                        room = await response.json();
                        console.log(room);
                        let loadworkers = document.getElementById("workers");                        
                        staticpaper = document.getElementById("imageView");
                        context = staticpaper.getContext('2d');
                        //cx = staticpaper.width / 2;
                        //если найдена такая комната, то запоминаем ее и выводим для редактирования
                        if (room.RoomId != 0) {
                            context.closePath();
                            context.beginPath();
                            context.strokeStyle = 'red';
                            context.arc(ev._x, ev._y, 9, 0, Math.PI * 2, true); 
                            context.stroke();
                            context.closePath();
                            document.getElementById("FloorId").value = room.FloorId;
                            document.getElementById("RoomId").value = room.RoomId;
                            document.getElementById("Name").value = room.Name;
                            document.getElementById("Description").value = room.Description;
                            document.getElementById("Timetable").value = room.Timetable;
                            document.getElementById("Phone").value = room.Phone;
                            document.getElementById("Mail").value = room.Site;
                            workers = room.Workers;
                            selectedx = ev._x - cx;
                            selectedy = cy - ev._y;
                            if (workers.length != 0)
                            {
                                for (var i = 0; i < workers.length; i++) {
                                    loadworkers.append(new Option(workers[i].FirstName + ' ' + workers[i].SecondName.substring(0, 1) + ".",
                                        workers[i].FirstName + workers[i].SecondName.substring(0, 1) + "."));
                                }
                            }
                            
                           
                        }
                        //иначе оставляем поля пустыми
                        else
                        {
                            document.getElementById("FloorId").value = null;
                            document.getElementById("RoomId").value = null;
                            document.getElementById("Name").value = null;
                            document.getElementById("Description").value = null;
                            document.getElementById("Timetable").value = null;
                            document.getElementById("Phone").value = null;
                            document.getElementById("Mail").value = null;
                            selectedx = null;
                            selectedy = null;
                            loadworkers.innerText = null
                        }
                        
                    };
                    GetRooms();


                }
            };
        };
        //перенос изображения
        tools.moveimg = function () {
            var tool = this;
            tool.started = false;

            this.mousedown = function (ev) {
                tool.x0 = ev._x;
                tool.y0 = ev._y;
                tool.started = true;
                //Если перенос, то выбираем точку
                if (ctrl) {
                    for (var k = 0; k < points.length; k++)
                        for (var i = -5; i < 6; i++)
                            for (var j = -5; j < 6; j++) {
                                if (points[k].X === tool.x0 - cx + i && points[k].Y === cy - tool.y0 + j) {
                                    if (!points[k].IsWaypoint) {
                                        selectedid = points[k].Id;
                                        number = k;
                                        selectedpoint = points[k];
                                        break;
                                    }
                                }
                            }
                }
                else {
                    //если удалить, то выбираем точку
                    if (shift) {
                        //выбрана  ли хоть одна точка
                    let perem = false;
                    for (var k = 0; k < points.length; k++)
                        for (var i = -5; i < 6; i++)
                            for (var j = -5; j < 6; j++) {
                                if (points[k].X === tool.x0 - cx + i && points[k].Y === cy - tool.y0 + j) {
                                    if (!points[k].IsWaypoint) {
                                        pointtodel = points[k];
                                        perem = true;
                                        break;
                                    }
                                }
                            }
                        //если да, то присваиваем ей координаты
                    if (perem)
                    {
                        delx = ev._x - cx;
                        dely = cy - ev._y;
                        context.closePath();
                        context.beginPath();
                        context.strokeStyle = 'red';
                        context.arc(ev._x, ev._y, 9, 0, Math.PI * 2, true); 
                        context.stroke();
                        context.closePath();
                        
                    }   
                    }
                }
           };

            this.mousemove = function (ev) {
                if (tool.started) {
                    //если двигаем все точки
                    if (!ctrl) {
                        //измереяем разницу в переносе
                        cx += ev._x - tool.x0;
                        cy += ev._y - tool.y0;
                        clinex += -ev._x + tool.x0;
                        cliney += -ev._y + tool.y0;
                        //очищаем временный канвас
                        contextdraw.clearRect(0, 0, canvas.width, canvas.height);
                        // и на нем рисуем временные точки
                        edges.forEach(function (item, i, edges) {
                            if (item.PointFrom.IsWaypoint) contextdraw.strokeStyle = 'green';
                            else contextdraw.strokeStyle = 'black';
                            contextdraw.beginPath();
                            let xx = item.PointFrom.X + cx;
                            let yy = cy - item.PointFrom.Y;
                            let xxx = item.PointTo.X + cx;
                            let yyy = cy - item.PointTo.Y;
                            contextdraw.moveTo(xx, yy);
                            contextdraw.lineTo(xxx, yyy);
                            contextdraw.stroke();

                        });
                        points.forEach(function (item, i, points) {
                            contextdraw.beginPath();
                            if (item.IsWaypoint) {
                                contextdraw.strokeStyle = 'orange';
                                contextdraw.arc(item.X + cx, cy - item.Y, 3, 0, Math.PI * 2, true); 
                                contextdraw.stroke();
                            }
                            else {
                                contextdraw.strokeStyle = 'black';
                                contextdraw.arc(item.X + cx, cy - item.Y, 5, 0, Math.PI * 2, true); 
                                contextdraw.stroke();
                            }
                        });
                        //координаты для следующего раза
                        tool.x0 = ev._x;
                        tool.y0 = ev._y;
                        //рисуем центр на канве и делаем перенос
                        contextdraw.beginPath();
                        contextdraw.strokeStyle = 'navy';
                        contextdraw.arc(cx, cy, 2, 0, Math.PI * 2, true); //центр
                        contextdraw.stroke();
                        axis();
                        context.clearRect(0, 0, canvas.width, canvas.height);
                        context.drawImage(canvas, 0, 0);
                        //и затираем временный
                        contextdraw.clearRect(0, 0, canvas.width, canvas.height);
                        //если есть комната или точка на удаление,то их тоже отображаем
                        if (room != null)
                        {
                            context.closePath();
                            context.beginPath();
                            context.strokeStyle = 'red';
                            context.arc(selectedx + cx, cy - selectedy, 9, 0, Math.PI * 2, true); //центр
                            context.stroke();
                            context.closePath();
                        }
                        if (pointtodel != null) {
                            context.closePath();
                            context.beginPath();
                            context.strokeStyle = 'red';
                            context.arc(delx + cx, cy - dely, 9, 0, Math.PI * 2, true); //центр
                            context.stroke();
                            context.closePath();
                        }
                    }
                    //если двигаем только одну точку
                    else
                    {
                        //изменяем ее координаты
                        points[number].X = ev.layerX - cx;
                        points[number].Y = cy - ev.layerY;
                        //и грани которые связаны
                        edges.forEach(function (item, i, edges) {
                            if (item.PointFrom.Id == selectedid) {
                                item.PointFrom.X = points[number].X;
                                item.PointFrom.Y = points[number].Y;
                            }
                            if (item.PointTo.Id == selectedid) {
                                item.PointTo.X = points[number].X;
                                item.PointTo.Y = points[number].Y;
                            }
                        });
                        //очищаем временный канвас
                        contextdraw.clearRect(0, 0, canvas.width, canvas.height);
                        // и на нем рисуем временные точки+ точку на удаление если есть
                        edges.forEach(function (item, i, edges) {
                            if (item.PointFrom.IsWaypoint) contextdraw.strokeStyle = 'green';
                            else contextdraw.strokeStyle = 'black';
                            contextdraw.beginPath();
                            let xx = item.PointFrom.X + cx;
                            let yy = cy - item.PointFrom.Y;
                            let xxx = item.PointTo.X + cx;
                            let yyy = cy - item.PointTo.Y;
                            contextdraw.moveTo(xx, yy);
                            contextdraw.lineTo(xxx, yyy);
                            contextdraw.stroke();

                        });
                        points.forEach(function (item, i, points) {
                            contextdraw.beginPath();
                            if (item.IsWaypoint)
                            {
                                contextdraw.strokeStyle = 'orange';
                                contextdraw.arc(item.X + cx, cy - item.Y, 3, 0, Math.PI * 2, true); //центр
                                contextdraw.stroke();
                            }
                            else
                            {
                                contextdraw.strokeStyle = 'black';
                                contextdraw.arc(item.X + cx, cy - item.Y, 5, 0, Math.PI * 2, true); //центр
                                contextdraw.stroke();
                            }
                            //если переносится точка на удаление, то ее тоже перерисовываем
                            if (pointtodel != null && pointtodel.Id == item.Id)
                            {
                                delx = item.X;
                                dely = item.Y;
                                context.closePath();
                                context.beginPath();
                                context.strokeStyle = 'red';
                                context.arc(delx + cx, cy - dely, 9, 0, Math.PI * 2, true); //центр
                                context.stroke();
                                context.closePath();
                            }
                        });
                        //рисуем центр и делаем перенос
                        contextdraw.beginPath();
                        contextdraw.strokeStyle = 'navy';
                        contextdraw.arc(cx, cy, 2, 0, Math.PI * 2, true); //центр
                        contextdraw.stroke();
                        context.clearRect(0, 0, canvas.width, canvas.height);
                        context.drawImage(canvas, 0, 0);
                        contextdraw.clearRect(0, 0, canvas.width, canvas.height);
                        //если есть комната и точка на удаление, то перерисовываем их
                        if (room != null)
                        {
                            context.closePath();
                            context.beginPath();
                            context.strokeStyle = 'red';
                            context.arc(selectedx + cx, cy - selectedy, 9, 0, Math.PI * 2, true); //центр
                            context.stroke();
                            context.closePath();
                        }
                        if (pointtodel != null)
                        {
                            context.closePath();
                            context.beginPath();
                            context.strokeStyle = 'red';
                            context.arc(delx + cx, cy - dely, 9, 0, Math.PI * 2, true); //центр
                            context.stroke();
                            context.closePath();
                        }   
                    }   
                }
            };

            this.mouseup = function (ev) {
                if (tool.started) {
                    //пост запрос на передвижение
                    tool.started = false;
                    if (selectedpoint != null) {
                        $.ajax({
                            url: '/Points/Move',
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify(
                                selectedpoint
                            ),
                            complete: function () {
                               // alert('Load was performed.');
                            }
                        });
                    }
                    //забываем информацию о том что мы перетаскивали
                    number = null;
                    selectedid = null;
                    selectedpoint = null;
                    //img_update();
                }
                //$('#imageTemp').mouseup
                //{
                //    new function () {
                //        //$.get(,)
                //        //var pointt = { firstx: ev._x, firsty: ev._y, secondx: ev._x, secondy: ev._y }
                //        let n = document.getElementById("dtool").options.selectedIndex;
                //        alert();
                //        if (n == 0) {
                //            $.ajax({
                //                url: '/Points/Line',
                //                type: "POST",
                //                contentType: "application/json; charset=utf-8",
                //                dataType: "json",
                //                data: JSON.stringify({
                //                    firstx: ev._x - cx, firsty: cy - ev._y, secondx: mdx - cx, secondy: cy - mdy, level: lvl.options[lvl.selectedIndex].text,
                //                    id: id.innerHTML
                //                }),
                //                complete: function () {
                //                    alert('Load was performed.');
                //                }
                //            });
                //        }
                //    }

                //}
            };
        };
        //вызов инициализаци
        init();

    }, false);
}

document.body.querySelector('#spisok').addEventListener('change', event => {
    let x;
    //выбор этажа
    for (opt of event.target.children) {
        if (opt.selected) {
            selectedx = null;
            selectedy = null;
            delx = null;
            dely = null;
            pointtodel = null;
            room = null;
            //let response = await fetch(url);
            //console.log(response);
            //points = await response.json();
            x = opt.value
            //console.log(opt.value);
            break;
        }
    }
    x = Number(x.substring(0, x.indexOf(' ')));
    document.getElementById("Floorlevel").innerHTML = x;
    updatedraw();
}, false);
addEventListener("keydown", function () {
    //console.log(this.event.keyCode);
    switch (this.event.keyCode) {
        case 17:
            if (!ctrl) {
                console.log("ctrl");
                ctrl = true;
            }
            break;
        case 16:
            if (!shift)
            {
                console.log("shift");
                shift = true;
            }
            break;
        case 46:
            if (pointtodel) {
                console.log("DEL");
                $.ajax({
                    url: '/Points/Del',
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(
                        pointtodel
                    ),
                    complete: function () {
                        alert('Load was performed.');
                    }
                });
                pointtodel = null;
                delx = null;
                dely = null;
                updatedraw();
            }
            break;
        default:
            
    }
});
addEventListener("keyup", function () {
    //console.log(this.event.keyCode);
    ctrl = false;
    shift = false;
    console.log("otpusk");
});
function Button() {
    if (document.getElementById("RoomId").value != null) {
        //room.FloorId = document.getElementById("FloorId").value
        room.Name = document.getElementById("Name").value;
        room.Description = document.getElementById("Description").value;
        room.Timetable = document.getElementById("Timetable").value;
        room.Phone = document.getElementById("Phone").value;
        room.Site = document.getElementById("Mail").value;
        $.ajax({
            url: '/Rooms/Edit',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(room),
            complete: function () {
                //alert('Load was performed.');
            }
        });
         //alert("ne pusto");
    }
    //else alert("pusto");
}
function Copy() {
    if (document.getElementById("Floorlevel").innerHTML != null) {
        //room.FloorId = document.getElementById("FloorId").value
        //floor.level = document.getElementById("Floorlevel").value;
        //floor.BuildingId = document.getElementById("BuildingId").value
        $.ajax({
            url: '/Floors/CopyFloor',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                Level: x,
                BuildingId: id  
            }),
            complete: function () {
                window.location.reload();
            }
        });
        //alert("ne pusto");
    }
    //else alert("pusto");
}
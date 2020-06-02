//let canvas = document.getElementById("imageView");

//let ctx = canvas.getContext('2d');
//ctx.fillStyle = "blue";
//ctx.beginPath();
//ctx.arc(75, 75, 50, 0, Math.PI * 2, true);
//ctx.fill();
//ctx.moveTo(110, 75);
//ctx.arc(75, 75, 35, 0, Math.PI, false);
//ctx.closePath()
//ctx.stroke();
//alert('Добрый день');
function draw(){
    {///загрузка линий и их рисование
        async function f() {
            let response = await fetch(url);
            console.log(response);
            floor = null;
            floor = await response.json();
            console.log(floor);
            staticpaper = document.getElementById("imageView");
            context = staticpaper.getContext('2d');
            floor.forEach(function (item, i, floor)
            {
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
            });
        }
        ///загрузка точек и их рисование
        async function func() {
            let response = await fetch(url);
            console.log(response);
            points = null;
            points = await response.json();
            console.log(points);
            staticpaper = document.getElementById("imageView");
            context = staticpaper.getContext('2d');

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

            }
            );
            context.strokeStyle = 'black';
        }
        let id = document.getElementById("BuildingId").innerHTML;
        let x = document.getElementById("FloorId").innerHTML;
        console.log(x);
        let url = "https://localhost:44336/api/Floors/Edges?level=" + x + '&&id=' + id;
        console.log(url);
        f();
        url = "https://localhost:44336/api/PointMs/FloorPoints?level=" + x + '&&id=' + id;


        func();

}
var points, floor; var cx, cy;
var selectedx, selectedy;
var selectedpoint;
var selectedid, number, pointtodel;
var delx, dely;
let ctrl = false;
let shift = false;
var room;
var canvas; var contextdraw;//temp
var staticpaper; var context;//view
if (window.addEventListener) {
    window.addEventListener('load', function () {

        //if (window.screen.availHeight > 720 &&
        //    window.screen.availWidth > 1366)
        //{
        //    var first = document.getElementById("first");
        //    var second = document.getElementById("second");
        //    var third = document.getElementById("third");
        //    second.classList.remove("col-lg-offset-1");
        //    second.classList.remove("col-lg-5");
        //    second.classList.add("col-lg-7");
        //    third.classList.remove("col-lg-4");
        //    third.classList.add("col-lg-3");
        //    document.getElementById("container").width = 900;
        //    document.getElementById("imageView").width = 900;
        //    document.getElementById("imageTemp").width = 900;

        //}

        var tool;
        var tool_default = 'line';
        
        var mdx, mdy;
        var centerx, centery;
        var lvl, id;

        function init() {
            id = document.getElementById("BuildingId");
            lvl = document.getElementById("spisok");
            canvas = document.getElementById("imageTemp");
            staticpaper = document.getElementById("imageView");
            centerx = document.getElementById("centerx");
            centery = document.getElementById("centery");
            if (canvas) {
                contextdraw = canvas.getContext('2d');
                context = staticpaper.getContext('2d');
                contextdraw.beginPath();
                contextdraw.strokeStyle = 'orange';
                cx = canvas.width / 2;
                cy = canvas.height / 2;
                contextdraw.arc(cx, cy, 2, 0, Math.PI * 2, true); //центр
                contextdraw.arc(cx, cy, 400, 0, Math.PI * 2, true); //центр
                contextdraw.stroke();
                //context.drawImage(canvas, 10, 10);
                //contextdraw.drawImage(canvas, -10, -10);
                //contextdraw.closePath();
                img_update();
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
        function center_update() {

        }
        function img_update() {
            //context.clearRect(0, 0, canvas.width, canvas.height);
            center_update();
            context.drawImage(canvas, 0, 0);
            contextdraw.clearRect(0, 0, canvas.width, canvas.height);
        }
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

        function tool_change(ev) {
            if (tools[this.value]) {
                tool = new tools[this.value]();
            }
        }
        var tools = {};

        tools.line = function () {
            var tool = this;
            tool.started = false;
            tool.perenos = false;

            this.mousedown = function (ev) {

                if (ev.button == 0) {
                    tool.started = true;
                    tool.x0 = ev._x;
                    tool.y0 = ev._y;
                }
                if (ev.button == 2) {
                    tool.perenos = true
                }

            };

            this.mousemove = function (ev) {
                if (!tool.started) {
                    if (!tool.perenos) { return; }
                    else {

                    }
                    return;
                }

                contextdraw.clearRect(0, 0, canvas.width, canvas.height);
                mdx = tool.x0;
                mdy = tool.y0;
                contextdraw.beginPath();
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

            };

            this.mouseup = function (ev) {
                if (tool.started) {
                    tool.mousemove(ev);
                    //contexto.arc(tool.mousemove(ev).x0, tool.mousemove(ev).x0,50, 0, 2 * Math.PI, false)
                    tool.started = false;
                    img_update();
                }
                if (tool.perenos) {
                    tool.perenos = false;
                }
                $('#imageTemp').mouseup
                {
                    new function () {
                        //$.get(,)
                        //var pointt = { firstx: ev._x, firsty: ev._y, secondx: ev._x, secondy: ev._y }
                        let n = document.getElementById("dtool").options.selectedIndex;
                        alert();
                        if (n == 0) {
                            $.ajax({
                                url: '/Points/Line',
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: JSON.stringify({
                                    firstx: ev._x - cx, firsty: cy - ev._y, secondx: mdx - cx, secondy: cy - mdy, level: lvl.options[lvl.selectedIndex].text,
                                    id: id.innerHTML
                                }),
                                complete: function () {
                                    alert('Load was performed.');
                                }
                            });
                        }
                    }

                }
            };
        };

        tools.rect = function () {
            var tool = this;
            this.started = false;

            this.mousedown = function (ev) {
                tool.started = true;
                tool.x0 = ev._x;
                tool.y0 = ev._y;
            };

            this.mousemove = function (ev) {
                if (!tool.started) {
                    return;
                }

                var x = Math.min(ev._x, tool.x0),
                    y = Math.min(ev._y, tool.y0),
                    w = Math.abs(ev._x - tool.x0),
                    h = Math.abs(ev._y - tool.y0);

                contextdraw.clearRect(0, 0, canvas.width, canvas.height);

                if (!w || !h) {
                    return;
                }
                contextdraw.beginPath();
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
                    img_update();
                    mdx = tool.x0;
                    mdy = tool.y0;
                    $('#imageTemp').mouseup
                    {
                        new function () {
                            //$.get(,)
                            //var pointt = { firstx: ev._x, firsty: ev._y, secondx: ev._x, secondy: ev._y }
                            let n = document.getElementById("dtool").options.selectedIndex;
                            
                                if (n == 1)
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
                                        success: function () {

                                        }
                                    });
                            


                        }

                    }
                }

            };

        };

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
                    console.log(url);
                    async function GetRooms() {
                        let response = await fetch(url);
                        console.log(response);
                        room = await response.json();
                        console.log(room);
                        staticpaper = document.getElementById("imageView");
                        context = staticpaper.getContext('2d');
                        //cx = staticpaper.width / 2;
                        //cy = staticpaper.height / 2;
                        if (room.RoomId != 0) {
                            context.closePath();
                            context.beginPath();
                            context.strokeStyle = 'red';
                            context.arc(ev._x, ev._y, 9, 0, Math.PI * 2, true); //центр
                            context.stroke();
                            context.closePath();
                            document.getElementById("FloorId").value = room.FloorId;
                            document.getElementById("RoomId").value = room.RoomId;
                            document.getElementById("Name").value = room.Name;
                            document.getElementById("Description").value = room.Description;
                            document.getElementById("Timetable").value = room.Timetable;
                            document.getElementById("Phone").value = room.Phone;
                            document.getElementById("Mail").value = room.Site;
                            selectedx = ev._x - cx;
                            selectedy = cy-ev._y;
                        }
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
                        }
                        
                    };
                    GetRooms();

                }
            };

           
          

        };
        tools.moveimg = function () {
            var tool = this;
            tool.started = false;
            tool.perenos = false;

            this.mousedown = function (ev) {
                tool.x0 = ev._x;
                tool.y0 = ev._y;
                tool.started = true;
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
                    if (selectedpoint != null) {

                    }
                }
                else {
                    if (shift) {
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
                    if (perem)
                    {
                        delx = ev._x - cx;
                        dely = cy - ev._y;
                        context.closePath();
                        context.beginPath();
                        context.strokeStyle = 'red';
                        context.arc(ev._x, ev._y, 9, 0, Math.PI * 2, true); //центр
                        context.stroke();
                        context.closePath();
                        
                    }   
                    }
                }
           };

            this.mousemove = function (ev) {
                if (tool.started) {
                    if (!ctrl) {

                        cx += ev._x - tool.x0;
                        cy += ev._y - tool.y0;
                        contextdraw.clearRect(0, 0, canvas.width, canvas.height);

                        //contextdraw.drawImage(staticpaper, ev._x - tool.x0, ev._y - tool.y0);
                        //context.clearRect(0, 0, canvas.width, canvas.height);
                        //context.drawImage(canvas, 0, 0);
                        //contextdraw.clearRect(0, 0, canvas.width, canvas.height);

                        floor.forEach(function (item, i, floor) {
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
                                contextdraw.arc(item.X + cx, cy - item.Y, 3, 0, Math.PI * 2, true); //центр
                                contextdraw.stroke();
                            }
                            else {
                                contextdraw.strokeStyle = 'black';
                                contextdraw.arc(item.X + cx, cy - item.Y, 5, 0, Math.PI * 2, true); //центр
                                contextdraw.stroke();
                            }
                        });
                        tool.x0 = ev._x;
                        tool.y0 = ev._y;
                        contextdraw.beginPath();
                        contextdraw.strokeStyle = 'orange';
                        contextdraw.arc(cx, cy, 2, 0, Math.PI * 2, true); //центр
                        contextdraw.stroke();
                        context.clearRect(0, 0, canvas.width, canvas.height);
                        context.drawImage(canvas, 0, 0);
                        contextdraw.clearRect(0, 0, canvas.width, canvas.height);
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
                    else
                    {
                        points[number].X = ev.layerX - cx;
                        points[number].Y = cy - ev.layerY;
                        floor.forEach(function (item, i, floor) {
                            if (item.PointFrom.Id == selectedid) {
                                item.PointFrom.X = points[number].X;
                                item.PointFrom.Y = points[number].Y;
                            }
                            if (item.PointTo.Id == selectedid) {
                                item.PointTo.X = points[number].X;
                                item.PointTo.Y = points[number].Y;
                            }
                        });
                            contextdraw.clearRect(0, 0, canvas.width, canvas.height);

                            floor.forEach(function (item, i, floor) {
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
                                    contextdraw.arc(item.X + cx, cy - item.Y, 3, 0, Math.PI * 2, true); //центр
                                    contextdraw.stroke();
                                }
                                else {
                                    contextdraw.strokeStyle = 'black';
                                    contextdraw.arc(item.X + cx, cy - item.Y, 5, 0, Math.PI * 2, true); //центр
                                    contextdraw.stroke();
                                }
                                if (pointtodel!=null && pointtodel.Id == item.Id) {
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
                            tool.x0 = ev._x;
                            tool.y0 = ev._y;
                            contextdraw.beginPath();
                            contextdraw.strokeStyle = 'orange';
                            contextdraw.arc(cx, cy, 2, 0, Math.PI * 2, true); //центр
                            contextdraw.stroke();
                            context.clearRect(0, 0, canvas.width, canvas.height);
                            context.drawImage(canvas, 0, 0);
                            contextdraw.clearRect(0, 0, canvas.width, canvas.height);
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
                //this.mousedown();
                //mdx = tool.x0;
                //mdy = tool.y0;
                //
                //contextdraw.moveTo(tool.x0, tool.y0);
                //contextdraw.lineTo(ev._x, ev._y);
                //contextdraw.stroke(); contextdraw.closePath();
                //contextdraw.beginPath();
                //contextdraw.moveTo(tool.x0 + 3, tool.y0);
                //contextdraw.arc(tool.x0, tool.y0, 4, 0, Math.PI * 2, true); // Внешняя окружность
                //contextdraw.moveTo(ev._x + 3, ev._y);
                //contextdraw.arc(ev._x, ev._y, 4, 0, Math.PI * 2, true);
                //contextdraw.stroke();
                //contextdraw.closePath();

            };

            this.mouseup = function (ev) {
                if (tool.started) {
                    //contexto.arc(tool.mousemove(ev).x0, tool.mousemove(ev).x0,50, 0, 2 * Math.PI, false)
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
                                alert('Load was performed.');
                            }
                        });
                    }
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
        //$("imageTemp").mouseup(function () {

        //})
        init();

    }, false);
}

document.body.querySelector('#spisok').addEventListener('change', event => {
    let x;
    for (opt of event.target.children) {
        if (opt.selected) {
            selectedx = null;
            selectedy = null;
            delx = null;
            dely = null;
            //let response = await fetch(url);
            //console.log(response);
            //points = await response.json();
            x = opt.value
            //console.log(opt.value);
            break;
        }
    }
    let id = document.getElementById("BuildingId").innerHTML;
    x = Number(x.substring(0, x.indexOf(' ')));
    document.getElementById("FloorId").innerHTML = x;
    document.getElementById("Floorlevel").innerHTML = x;
    console.log(x);
    let url = "https://localhost:44336/api/Floors/Edges?level=" + x + '&&id=' + id;
    console.log(url);
    async function f() {
        let response = await fetch(url);
        console.log(response);
        floor = await response.json();
        console.log(floor);
        staticpaper = document.getElementById("imageView");
        context = staticpaper.getContext('2d');

        //let cx = staticpaper.width / 2;
        //let cy = staticpaper.height / 2;
        //context.clearRect(0, 0, cx*2, cy*2);
        floor.forEach(function (item, i, floor) {
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
    f();
    url = "https://localhost:44336/api/PointMs/FloorPoints?level=" + x + '&&id=' + id;
    async function func() {
        let response = await fetch(url);
        console.log(response);
        points = await response.json();
        console.log(points);
        staticpaper = document.getElementById("imageView");
        context = staticpaper.getContext('2d');
        //let cx = staticpaper.width / 2;
        //let cy = staticpaper.height / 2;
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

        }
        );
        context.strokeStyle = 'black';
    }


    func();

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
                {
                    let id = document.getElementById("BuildingId").innerHTML;
                    let x = document.getElementById("FloorId").innerHTML;
                    console.log(x);
                    let url = "https://localhost:44336/api/Floors/Edges?level=" + x + '&&id=' + id;
                    console.log(url);
                    async function f() {
                        let response = await fetch(url);
                        console.log(response);
                        floor = null;
                        floor = await response.json();
                        console.log(floor);
                        staticpaper = document.getElementById("imageView");
                        context = staticpaper.getContext('2d');
                        floor.forEach(function (item, i, floor) {
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
                    f();
                    url = "https://localhost:44336/api/PointMs/FloorPoints?level=" + x + '&&id=' + id;
                    async function func() {
                        let response = await fetch(url);
                        console.log(response);
                        points = null;
                        points = await response.json();
                        console.log(points);
                        staticpaper = document.getElementById("imageView");
                        context = staticpaper.getContext('2d');

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

                        }
                        );
                        context.strokeStyle = 'black';
                    }


                    func();

                }
                
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
                alert('Load was performed.');
            }
        });
         alert("ne pusto");
    }
    else alert("pusto");
    //document.getElementById("zona").innerHTML = "Молодец! Ты прошел испытание на смелость! ";
    //document.getElementById("zona").style.color = 'red';
   // document.getElementById("zona").style.fontSize = '32px';
}
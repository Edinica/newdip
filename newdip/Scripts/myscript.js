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
if (window.addEventListener) {
    window.addEventListener('load', function () {
        var canvas; var contextdraw;
        var staticpaper; var context;


        var tool;
        var tool_default = 'line';
        var cx, cy;
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
                contextdraw.strokeStyle = 'red';
                cx = canvas.width / 2;
                cy = canvas.height / 2;
                contextdraw.arc(cx, cy, 2, 0, Math.PI * 2, true); //центр
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
                    let url = "https://localhost:44336/api/Rooms?level=" + x + '&&id=' + id + '&&x=' + (ev._x - cx) + '&&y=' + (cy - ev._y);
                    console.log(url);
                    async function GetRooms() {
                        let response = await fetch(url);
                        console.log(response);
                        let floor = await response.json();
                        console.log(floor);
                        staticpaper = document.getElementById("imageView");
                        context = staticpaper.getContext('2d');
                        let cx = staticpaper.width / 2;
                        let cy = staticpaper.height / 2;
                        //context.clearRect(0, 0, cx*2, cy*2);
                        floor.forEach(function (item, i, floor) {
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
                    };
                    GetRooms();
                    //new function () {
                    //    //$.get(,)
                    //    //var pointt = { firstx: ev._x, firsty: ev._y, secondx: ev._x, secondy: ev._y }
                    //    //let n = document.getElementById("dtool").options.selectedIndex;

                    //        $.ajax({
                    //            url: '/Points/Room',
                    //            type: "POST",
                    //            contentType: "application/json; charset=utf-8",
                    //            dataType: "json",
                    //            data: JSON.stringify({
                    //                firstx: ev._x - cx, firsty: cy - ev._y,
                    //                level: lvl.options[lvl.selectedIndex].text,
                    //                id: id.innerHTML
                    //            }),
                    //            complete: function (data) {
                    //                alert();
                    //                console.log(data);
                    //                let response = data.json();
                    //                console.log(response);
                    //            }
                    //        });



                    //}

                }
            };

           
            //this.mouseup = function (ev) {
            //    if (tool.started) {
            //        tool.mousemove(ev);
            //        //contexto.arc(tool.mousemove(ev).x0, tool.mousemove(ev).x0,50, 0, 2 * Math.PI, false)
            //        tool.started = false;
            //        img_update();
            //        mdx = tool.x0;
            //        mdy = tool.y0;
            //        $('#imageTemp').mouseup
            //        {
            //            new function () {
            //                //$.get(,)
            //                //var pointt = { firstx: ev._x, firsty: ev._y, secondx: ev._x, secondy: ev._y }
            //                let n = document.getElementById("dtool").options.selectedIndex;

            //                if (n == 1)
            //                    $.ajax({
            //                        url: '/Points/AddRectangle',
            //                        type: "POST",
            //                        contentType: "application/json; charset=utf-8",
            //                        dataType: "json",
            //                        data: JSON.stringify({
            //                            firstx: ev._x - cx, firsty: cy - ev._y, secondx: mdx - cx, secondy: cy - ev._y,
            //                            thirdx: mdx - cx, thirdy: cy - mdy, fourthx: ev._x - cx, fourthy: cy - mdy,
            //                            level: lvl.options[lvl.selectedIndex].text,
            //                            id: id.innerHTML
            //                        }),
            //                        success: function () {

            //                        }
            //                    });



            //            }

            //        }
            //    }

            //};

        };
        //$("imageTemp").mouseup(function () {

        //})
        init();

    }, false);
}

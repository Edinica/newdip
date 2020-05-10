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
                }

            };

        };

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
                        $.ajax({
                            url: '/Points/Line',
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({
                                secondx: ev._x - cx, secondy: cy - ev._y, firstx: mdx - cx, firsty: cy - mdy, level: lvl.options[lvl.selectedIndex].text,
                                id: id.innerHTML
                            }),
                            success: function () {

                            }
                        });
                        //$.ajax({
                        //    url: '/Points/Addpoint', // также '@Url.Action("Addpoint", "PointsController")'
                        //    type: "GET",
                        //    dataType: "json",
                        //    data: JSON.stringify({ firstx: ev._x, firsty: ev._y, secondx: ev._x, secondy: ev._y }),
                        //    success: function () {
                        //        alert();
                        //    }
                        //});

                    }

                }
            };
        };

        //$("imageTemp").mouseup(function () {

        //})
        init();

    }, false);
}

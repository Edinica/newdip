document.body.querySelector('#spisok').addEventListener('change', event => {
    let x;
    for (opt of event.target.children) {
        if (opt.selected) {
            x = opt.value
            //console.log(opt.value);
            break;
        }
    }
    let id = document.getElementById("BuildingId").innerHTML;
    x = Number(x.substring(0, x.indexOf(' ')));
    console.log(x);
    let url = "https://localhost:44336/api/Floors?level=" + x + '&&id=' + id;
    console.log(url);
    async function f() {
        let response = await fetch(url);
        console.log(response);
        let floor = await response.json();
        console.log(floor);
        staticpaper = document.getElementById("imageView");
        context = staticpaper.getContext('2d');
        let cx = staticpaper.width / 2;
        let cy = staticpaper.height / 2;
        context.clearRect(0, 0, cx*2, cy*2);
        floor.forEach(function (item, i, floor)
        {
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
       
}, false);
function TabClick(id, url) {
    document.getElementById('F' + id).src = url;

    $("ul.abc li").removeClass("selected"); //Remove any "active" class
    $(this).addClass("selected"); //Add "active" class to selected tab


    //    alert(id);
    //    alert(url);
    //    var lad = 'Loading.htm';
    //    alert(document.getElementById('F' + id).src);
    //    alert(document.getElementById('F' + id).src.IndexOf('ing.htm'));
    //   if ( document.getElementById('F'+id ).src.indexof('ing.htm') >0 )
    //   {
    //       document.getElementById('F' + id).src = url;
    //       alert(url);
    //   }
}




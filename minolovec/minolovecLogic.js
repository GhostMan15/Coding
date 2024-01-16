var velikost
var zastavice;
var stMin;
function omogociIgro(){
    valikost = document.getElementById("velikostPloscadi").value;
    if(velikost = 0){
        document.getElementById("novaIgra").disabled = true;
        document.getElementById("IgralnoPolje").innerHTML = '';

        //ustavi merjenje casa
    }
    else document.getElementById("novaIgra").disabled = false;
    document.getElementById("IgralnoPolje").innerHTML = '';
}
function pozeniIgro(){
    zastavice = 0;
    //pobrisi podatke prejsnje igre
    document.getElementById("stevilkoZastavic").innerHTML = '';
    document.getElementById("casIgranja").innerHTML = '';
    var st_min = velikost - 8;
    stMin = Math.round(Math.random()*st_min)+8;
    document.getElementById("stevilkoMin").innerHTML = stMin;
    var str = '<table border="1">'
    for(var i=0; i<velikost; i++){
        str += '<tr heiht="40px">';
        for(var j=0; j<velikost; j++){
            str += '<td vallign="middle" allign="middle" width="40px">'+i+', '+j;

            str +='</td>';
        }
        str += '</tr>';
    }

    //doloci in izpisi stevilo min

    //pripravi igralno polje

    //razpodeli mine

    //dilici stike z minami

    //poišči prazno začetno polje

    //poženi uro
}
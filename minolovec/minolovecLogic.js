var velikost;
var zastavice;
var stMin;
var poljeMin = [];
var poljeOdkrito = [];
function omogociIgro(){
    velikost = Number(document.getElementById("velikostPloscadi").value);
    if(velikost == 0){
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

    //doloci in izpisi stevilo min
    var st_min = velikost - 8;
    stMin = Math.round(Math.random()*st_min)+8;
    document.getElementById("stevilkoMin").innerHTML = stMin;

    //pripravi igralno polje
    var str = '<table border="1">'
    for(var i=0; i<velikost; i++){
        str += '<tr height="40px">';
        for(var j=0; j<velikost; j++){
            str += '<td vallign = "middle" allign="middle" width="40px" id = "C'+i+'_'+j+'" ';
            str += 'onmouseover = this.style.cursor="pointer" onmouseout = this.style.cursor="default" ';
            str += 'onmousedown = "preveriGumb(event,'+(i*velikost+j)+')" oncontextmenu = "return false;">';
            str += '<img src="img/pokrito.png" width="40px"></td>';
        }
        str += '</tr>';
    }
    str += '<table>';
    document.getElementById("IgralnoPolje").innerHTML = str;

    //razpodeli mine
    poljeMin.length = velikost*velikost;
    for(var i=0; i<poljeMin.length; i++){
        poljeMin[i]=0;
        poljeOdkrito[i]=0;
    }
    st_min = stMin;
    while(st_min>0){
        var ind = Math.floor(Math.random()*poljeMin.length);
        if(poljeMin[ind] != 'M'){
            poljeMin[ind] = 'M';
            poljeOdkrito[ind] = 1;
            st_min--;
        }
    }

    //doloci stike z minami
    for(var i=0; i<poljeMin.length; i++){
        if(poljeMin[i] == 'M')
        {
            var stik = -1;
            for(var j=0; j<8; j++){
                switch(j){
                    case 0:
                        stik = i - velikost - 1;
                        if(i % velikost == 0)stik = -1;
                        break;
                    case 1:
                        stik = i - velikost;
                        break;
                    case 2:
                        stik = i - velikost + 1;
                        if((i+1) % velikost == 0)stik = -1;
                        break;
                    case 3:
                        stik = i - 1;
                        if(i % velikost == 0)stik = -1;
                        break;
                    case 4:
                        stik = i + 1;
                        if((i+1) % velikost == 0)stik = -1;
                        break;
                    case 5:
                        stik = i + velikost - 1;
                        if(i % velikost == 0)stik = -1;
                        break;
                    case 6:
                        stik = i + velikost;
                        break;
                    case 7:
                        stik = i + velikost + 1;
                        if((i+1) % velikost == 0)stik = -1;
                        break;

                }
                if(stik >= velikost*velikost) stik = -1;
                if(stik >= 0){
                    if(poljeMin[stik]!='M')poljeMin[stik]++;
                }
            }
        }
    }

    //poišči prazno začetno polje
    while(1){
        var ind = Math.floor(Math.random()*poljeMin.length);
        if(poljeMin[ind] == 0){
            poljeOdkrito[ind] = 1;
            var j = ind % velikost;
            var i = (ind - j) / velikost;
            document.getElementById('C'+i+'_'+j).innerHTML = '';
            break;
        }
    }

    //izrisi polje
    /*
    st_min = 0; 
    for(var i=0; i<velikost; i++){
        for(var j=0; j<velikost; j++){
            if(poljeMin[st_min] == 'M'){
                //izrisem mino
                document.getElementById('C'+i+'_'+j).innerHTML = '<img src="img/mina.jpg" width="40px">';
            }else{
                if(poljeMin[st_min] != 0){
                    document.getElementById('C'+i+'_'+j).innerHTML = poljeMin[st_min];
                }
            }
            st_min++;
        }
    }*/
    
    
    
    //poženi uro
    var interval;
    function pozeniTimer()
    {
        interval = setInterval(function (){izpisCasa();},1000);
    }
    function preveriGumb(event,stPolja)
    {
        if(event.button==0){
            poljeOdkrito[stPolja]=1;
            if(poljeMin[stPolja]=='M')
            {
                var j =stPolja%velikost;
                var i =(stPolja-j)/velikost;
                document.getElementById('C'+i+'_'+j).innerHTML = '<img src="img/mina.jpg" width="40px">';
                document.getElementById('Obvestilo').innerHTML = 'Odkril si mino.Igra je končana.';
                ustaviTimer();
            }
            else 
            {
                var vsebina = poljeMin[stPolja];
                if(vsebina=='0')vsebina='';
                document.getElementById('C'+i+'_'+j).innerHTML = vsebina;
            }
        }
        if(event.button==2){
            if(zastavice<stMin)
            {
                document.getElementById('C'+i+'_'+j).innerHTML = '<img src="img/zastavica.png" width="40px">';
                zastavice++;
                document.getElementById('C'+i+'_'+j).innerHTML = zastavice;
            }
        }
    }

    function preveriKonecIgre()
    {

    }
    var cas =0;
    function izpisCasa()
    {
        cas++;
        var min = (cas - sek)/60;
        var sek = cas%60;
        min +=':';
        if(sek<10)min='0';
        min+=sek;
        document.getElementById('casIgranja').innerHTML = min;

    }
    function ustaviTimer()
    {

    }
}
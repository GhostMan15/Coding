var velikost
var zastavice;
var stMin;
var poljeMin=[];
function omogociIgro(){
    velikost = document.getElementById("velikostPloscadi").value;
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
    var st_min = velikost - 8;
    stMin = Math.round(Math.random()*st_min)+8;
    document.getElementById("stevilkoMin").innerHTML = stMin;
    var str = '<table border="1">'
    for(var i=0; i<velikost; i++)
    {
        str += '<tr height="40px">';
        for(var j=0; j<velikost; j++)
        {
            str += '<td vallign="middle" allign="middle" width="40px"id="C'+i+'_'+j+'">'+i+', '+j;

            str +='</td>';
        }
        str += '</tr>';
    }
    document.getElementById("IgralnoPolje").innerHTML=str;

    poljeMin.length =velikost*velikost;
    st_min =stMin
    while(st_min>0)
    {
        var ind=Math.floor(Math.random()*poljeMin.length);
        if(poljeMin[ind]!='M')
        {
            poljeMin[ind]='M';
            st_min--;
        }
    }
    st_min=0;
    for(var i=0; i<velikost; i++)
    {
       for(var j=0; j<velikost; j++)
        {
            if(poljeMin[st_min]=='M')
            {
                document.getElementById('C'+i+'_'+j).innerHTML='<img src="img/mina.jpg" width="40px">';
            }
            st_min++;

        }
       
    }
}
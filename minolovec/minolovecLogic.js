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
    else document.getElementById("novaIgra").disabled = false;let maxNumber = 25; // Default max number for 5x5 grid

    function createBoard(player, columns) {
      let board = document.createElement('table');
      board.classList.add('board');
      board.dataset.player = player;
    
      let numbers = Array.from({ length: maxNumber }, (_, i) => i + 1);
      shuffle(numbers);
    
      for (let i = 0; i < maxNumber / columns; i++) {
        let row = document.createElement('tr');
        for (let j = 0; j < columns; j++) {
          let cell = document.createElement('td');
          cell.textContent = numbers[i * columns + j];
          row.appendChild(cell);
        }
        board.appendChild(row);
      }
    
      return board;
    }
    
    function createBoards() {
      let columnCount = parseInt(document.getElementById('columnCount').value);
      let playersDiv = document.getElementById('players');
      playersDiv.innerHTML = '';
    
      for (let i = 1; i <= 2; i++) {
        let playerDiv = document.createElement('div');
        playerDiv.id = `player${i}`;
        playerDiv.classList.add('player');
        playerDiv.innerHTML = `<h2>Player ${i}</h2>`;
        
        let board = createBoard(i, columnCount);
        playerDiv.appendChild(board);
        
        let rollButton = document.createElement('button');
        rollButton.textContent = 'Roll Dice';
        rollButton.onclick = () => rollDice(i, columnCount);
        playerDiv.appendChild(rollButton);
    
        playersDiv.appendChild(playerDiv);
      }
    }
    
    function rollDice(player, columns) {
      let playerBoard = document.querySelector(`.board[data-player="${player}"]`);
      let cells = playerBoard.getElementsByTagName('td');
    
      let availableNumbers = Array.from({ length: maxNumber }, (_, i) => i + 1)
        .filter(num => !Array.from(cells).some(cell => cell.textContent == num));
    
      if (availableNumbers.length === 0) {
        alert(`Player ${player} has no available numbers left!`);
        return;
      }
    
      let randomIndex = Math.floor(Math.random() * availableNumbers.length);
      let number = availableNumbers[randomIndex];
    
      let cell = Array.from(cells).find(cell => cell.textContent === '');
      cell.textContent = number;
    
      checkBingo(player, columns);
    }
    
    function checkBingo(player, columns) {
      let playerBoard = document.querySelector(`.board[data-player="${player}"]`);
      let rows = playerBoard.getElementsByTagName('tr');
    
      // Check rows
      for (let i = 0; i < maxNumber / columns; i++) {
        let row = rows[i];
        let cells = row.getElementsByTagName('td');
        if (Array.from(cells).every(cell => cell.textContent !== '')) {
          alert(`Bingo! Player ${player} wins!`);
          break;
        }
      }
    }
    
    function shuffle(array) {
      for (let i = array.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [array[i], array[j]] = [array[j], array[i]];
      }
    }
    
    // Initialize the boards with default column count
    createBoards();
    
    document.getElementById("IgralnoPolje").innerHTML = '';
}
function pozeniIgro(){
    zastavice = 0;
    document.getElementById("obvestilo").innerHTML = '';

    //pobrisi podatke prejsnje igre
    document.getElementById("stevilkoZastavic").innerHTML = '';
    document.getElementById("casIgranja").innerHTML = '';

    //doloci in izpisi stevilo min
    var st_min = velikost - 8;
    stMin = Math.round(Math.random()*st_min)+8;
    document.getElementById("stevilkoMin").innerHTML = stMin;

    //pripravi igralno polje
    var str = '<table cellspacing="0" cellpadding="0" border="0">'
    for(var i=0; i<velikost; i++){
        str += '<tr height="40px">';
        for(var j=0; j<velikost; j++){
            str += '<td vallign="middle" allign="middle" width="40px" id="C'+i+'_'+j+'" ';
            str += 'onmouseover = this.style.cursor="pointer" onmouseout = this.style.cursor="default" ';
            str += 'onmousedown = PreveriGumb(event,' + (i * velikost + j) + ') oncontextmenu="return false;">';
            str += '<img src="img/pokrito.png" width="40px">';
            str +='</td>';

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
        if(poljeMin[i] === 'M')
        {
            var stik = -1;
            for(var j=0; j<8; j++){
                switch(j){
                    case 0:
                        stik = i - velikost - 1;
                        if(i % velikost === 0)stik = -1;
                        break;
                    case 1:
                        stik = i - velikost;
                        break;
                    case 2:
                        stik = i - velikost + 1;
                        if((i+1) % velikost === 0)stik = -1;
                        break;
                    case 3:
                        stik = i - 1;
                        if(i % velikost === 0)stik = -1;
                        break;
                    case 4:
                        stik = i + 1;
                        if((i+1) % velikost === 0)stik = -1;
                        break;
                    case 5:
                        stik = i + velikost - 1;
                        if(i % velikost === 0)stik = -1;
                        break;
                    case 6:
                        stik = i + velikost;
                        break;
                    case 7:
                        stik = i + velikost + 1;
                        if((i+1) % velikost === 0)stik = -1;
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

    //poženi uro
    cas = 0;
    pozeniTimer();

    //izrisi polje

   /* st_min = 0;
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
}

var interval;
function pozeniTimer(){
    interval = setInterval(function(){izpisCasa();}, 1000);
    var klik_aktiven=1;
}
var klik_aktiven=0;
function ustaviTimer(){
    clearInterval(interval);
    var klik_aktiven=0;
}

var cas = 0;
function izpisCasa(){
    cas++;
    var sec = cas % 60;
    var min = (cas - sec)/60;
    min += ':'
    if(sec < 10)min+='0';
    min += sec;
    document.getElementById('casIgranja').innerHTML = min;
}

function PreveriGumb(event,stPolja){

        poljeOdkrito[stPolja] = 1;
        var j = stPolja % velikost;
        var i = (stPolja - j) / velikost;
        if(event.button === 0){
            if(poljeMin[stPolja] === 'M'){
                //konec igre
                document.getElementById('C'+i+'_'+j).innerHTML = '<img src="img/mina.jpg" width="40px">';
                document.getElementById('obvestilo').innerHTML = "Mrtu žuž";
                ustaviTimer();
            }else{
                    var vsebina = poljeMin[stPolja];
                    if(vsebina == '0')
                    {
                        vsebina = '';
                        razkrijSosednjaPolja(i, j);
                    }
                    document.getElementById('C'+i+'_'+j).innerHTML = vsebina;
                    PreveriKonecIgre();
            }
        }
        if(event.button === 2){
            if(zastavice < stMin){
                document.getElementById('C'+i+'_'+j).innerHTML = '<img src="img/zastavica.png" width="40px">';
                zastavice ++;
                document.getElementById('stevilkoZastavic').innerHTML = zastavice;
            }
        }
}

function PreveriKonecIgre(){
    var l = velikost * velikost;
    var neodkrito = 0;
    for(i = 0; i < l; i++){
        if(poljeOdkrito[i] === 0){
            neodkrito++;
            break;
        }
    }
    if(neodkrito === 0)
    {
        var str ='Čestitam izognil si se minam</br>';
        str +='<span onclick="">Natisni rezultat!</span>';
        document.getElementById('obvestilo').innerHTML = "Zgubu" ;
        ustaviTimer();
    }

}
function razkrijSosednjaPolja(vrst, stolp){
    for (let v = vrst - 1; v <= vrst + 1; v++){
        for (let s = stolp - 1; s <= stolp + 1; s++){
            if (v >= 0 && v < velikost && s >= 0 && s < velikost){
                let index = v * velikost + s;
                if (poljeOdkrito[index] == 0) {
                    poljeOdkrito[index] = 1;
                    if (poljeMin[index] == '0'){
                        document.getElementById('C' + v + '_' + s).innerHTML = '';
                        razkrijSosednjaPolja(v, s);
                    }
                    else{
                        document.getElementById('C' + v + '_' + s).innerHTML = poljeMin[index];
                    }
                }
            }
        }
    }
}

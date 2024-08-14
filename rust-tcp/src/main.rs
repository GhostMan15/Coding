use std::io::{Read, Write};
use std::net::{TcpListener, TcpStream};


fn handle_client(mut stream: TcpStream){
    let mut buffer = [0; 1024];
    stream.read(&mut buffer).expect("Failal prebat clienta");

    let request = String::from_utf8_lossy(&buffer[..]);
    println!("Recived req{request}");
    let respond = "Heeej".as_bytes();
    stream.write(respond).expect("Failal zapisat repond");
}

fn main() {
    let listener = TcpListener::bind("127.0.0.1:8080").expect("Failal se povezat");
    println!("Server poslusa");

    for stream in listener.incoming(){
        match stream {
            Ok(stream) => {
                std::thread::spawn(|| handle_client(stream));
            }
            Err(e) =>{
                eprintln!("Failal se povezat {e}");
            }
        }
    }
    
}


use std::{char, fs::{self, OpenOptions}};
use rand::Rng;
use std::io::Write;

fn main(){
    const CHARSET: &[u8] = b"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789)(*?!#$%&=";
    const geslo_len: u8 = 6; 
    let pot = "/home/faruk/Documents/GitHub/Coding/pass-randomizer/gesla";
      //Kreiranje gesla
    let mut rng = rand::thread_rng();
    let password: String = (0..geslo_len).map(|_|{
        let idx = rng.gen_range(0..CHARSET.len());
        CHARSET[idx] as char
    })
    .collect();


    //Če že obstaja geslo ponovno rekreiranje
    if password == pot{
        let _ = password;
    }

    //Zapisovanje gesla med ostala gesla 
    let mut file = OpenOptions::new()
        .append(true)
        .create(true)
        .open(pot)
        .expect("Ne morm ofnat"); 
    write!(file,"{}\n", password).expect("Ne delujeoč");
    let contet = fs::read_to_string(pot);
    println!("{:?}", contet);
}


use std::{char, fs::{self, OpenOptions}};
use rand::Rng;
use std::io::Write;


fn randomizer(charset :&[u8],  dolzina:u8) -> String{
 let mut rng = rand::thread_rng();
    let password: String = (0..dolzina).map(|_|{
        let idx = rng.gen_range(0..charset.len());
        charset[idx] as char
    })
    .collect();
    return password;
}

fn main(){
    const CHARSET: &[u8] = b"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789)(*?!#$%&=";
    const GESLO_LEN: u8 = 6; 
    let pot = "/home/faruk/Documents/GitHub/Coding/pass-randomizer/gesla";
      //Kreiranje gesla
    let ustvari = randomizer(CHARSET, GESLO_LEN);

    //Zapisovanje gesla med ostala gesla 
    let mut file = OpenOptions::new()
        .append(true)
        .create(true)
        .open(pot)
        .expect("Ne morm ofnat"); 
    write!(file,"{}\n", ustvari).expect("Ne delujeoč");
    let contet = fs::read_to_string(pot).expect("Ni fajla");

    //Če že obstaja geslo ponovno rekreiranje
    if contet == ustvari{
        let _ = ustvari;
    }
    println!("{:?}", contet);
   
}

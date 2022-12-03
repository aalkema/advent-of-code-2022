use std::fs::File;
use std::io::BufReader;
use std::io::prelude::*;
use std::collections::HashMap;

fn main() {
    let file = File::open("input.txt")
        .expect("File not found");
    let buf_reader = BufReader::new(file);

    let mut score: i32 = 0;
    for line in buf_reader.lines() {
        if let Ok(game) = line {
            let plays: Vec<&str> = game.split_whitespace().collect();
            score = score + 
        }
    }
}

fn get_points_for_move(move: &str) -> i32 {

}
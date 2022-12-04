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
            score = score + get_points_for_move(plays[1]);
            score = score + get_points_for_result(plays[0], plays[1]);
        }
    }

    println!("{}", score);
}

fn get_points_for_move(our_move: &str) -> i32 {
    let move_points = HashMap::from([
        ("X", 1),
        ("Y", 2),
        ("Z", 3),
    ]);
    return move_points[our_move];
}

fn get_points_for_result(opponent_play: &str, our_play: &str) -> i32 {
    let game_results = HashMap::from([
        ("AX", 3),
        ("AY", 0),
        ("AZ", 0),
        ("BX", 0),
        ("BY", 3),
        ("BZ", 6),
        ("CX", 6),
        ("CY", 0),
        ("CZ", 3),
    ]);

    let mut game = String::new();
    game.push_str(opponent_play);
    game.push_str(our_play);

    return game_results[&game];
}
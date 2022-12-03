use std::fs::File;
use std::io::BufReader;
use std::io::prelude::*;

fn main() {
    let file = File::open("input.txt")
        .expect("File not found");
    let buf_reader = BufReader::new(file);

    let mut current_top: [i32;3] = [0,0,0];
    let mut temp: i32 = 0;
    for line in buf_reader.lines() {
        if let Ok(calorie) = line {
            if calorie.is_empty() {
                add_to_ranked_list(temp, &mut current_top);
                temp = 0;
            } else {
                let num: i32 = calorie.parse().unwrap();
                temp = temp + num;
            }
        }
    }

    let mut total: i32 = 0;
    for i in 0..3 {
        total = total + current_top[i];
    }
    println!("{}", total);
}

fn add_to_ranked_list(elves_calories: i32, top_calories: &mut [i32;3]) {
    let mut current: i32 = elves_calories;
    for i in 0..3 {
        if current > top_calories[i] {
            let temp = top_calories[i];
            top_calories[i] = current;
            current = temp;
        }
    }
}
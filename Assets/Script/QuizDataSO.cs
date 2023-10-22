using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizDataSO", menuName = "Modul-Trivia-Quiz-Game/QuizDataSO", order = 0)]
public class QuizDataSO : ScriptableObject 
{
    public List<Question> questions;    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private List<TileInfo> _tiles;
    private List<TileInfoStar> _starTiles = new List<TileInfoStar>();
    private int _starScore;
    private int starScore
    {
        set
        {
            _starScore = value;
            _starScoreText.text = _starScore.ToString();
        }
    }
    [SerializeField] private Text _starScoreText;
    private int _diceNum;
    private int _goldenDiceNum;
    private int _tilesCount;
    private int _current;

    public void RollADice()
    {
        if (_diceNum > 0)
        {
            int randomValue = Random.Range(1, 7);
            MovePlayer(randomValue);
        }
    }

    private void MovePlayer(int diceValue)
    {
        _current += diceValue;
        if (_current >= _tilesCount)
            _current -= _tilesCount;

        Player.instance.MoveTo(_tiles[_current].transform);
        _tiles[_current].OnTile();
    }

    private void Awake()
    {
        instance = this;
        _diceNum = Constants.DICE_NUM_INIT;
        _goldenDiceNum = Constants.GOLDEN_DICE_NUM_INIT;
        _tiles.Sort();
        //_tiles.OrderBy(x => x.index);
        _tilesCount = _tiles.Count;
        foreach (TileInfo tile in _tiles)
        {
            //// is 연산자 
            //// 캐스트 후의 결과 반환하는 연산자
            //// 캐스팅 성공하면 true , 실패하면 false
            //if (tile is TileInfoStar)
            //{
            //    _starTiles.Add((TileInfoStar)tile);
            //}

            // as 명시적 캐스팅연산자
            // 형변환 실패시 null 반환
            TileInfoStar tmp = tile as TileInfoStar;
            if (tmp != null)
                _starTiles.Add(tmp);
            else
                throw new System.Exception("샛별칸 캐스팅 실패");
        }
    }
}

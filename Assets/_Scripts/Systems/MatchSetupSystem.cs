using System.Collections.Generic;
using UnityEngine;

public class MatchSystem : MonoBehaviour
{
    [SerializeField] private HeroData heroData;
    [SerializeField] private List<EnemyData> enemyDatas;
    private void Start()
    {
        HeroSystem.Instance.Setup(heroData);
        EnemySystem.Instance.Setup(enemyDatas);
        CardSystem.Instance.Setup(heroData.Deck);
        RefillManaGA refillManaGA = new();
        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.Perform(refillManaGA, () => ActionSystem.Instance.Perform(drawCardsGA));
    }
}

using UnityEngine;

public class HeroSystem : Singelton<HeroSystem>
{
    [field: SerializeField] public HeroView HeroView { get; private set; }
    public void Setup(HeroData heroData)
    {
        HeroView.Setup(heroData);
    }
}

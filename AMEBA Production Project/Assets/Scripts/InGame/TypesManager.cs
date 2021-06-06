using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemies { SALMONELLA, HEPATITIS_B, CELULAS_CANCERIGENAS, NISSERIA_GONORRHOEAE, HIV, INFLUENZA }
public enum Defenses { NEUTROFILO, LINFOCITO_B, LINFOCITO_T, EOSINOFILO, MONOCITO, BASOFILO }
public enum BattleState { DefenseWins, EnemyWins, Neutral }

public class TypesManager : Singleton<TypesManager>
{
    public BattleState DefenseDestroysEnemy(Enemies enemy, Defenses defense)
    {
        switch(enemy)
        {
            case Enemies.CELULAS_CANCERIGENAS:
                if (defense == Defenses.MONOCITO) 
                    return BattleState.DefenseWins;
                else 
                    return BattleState.EnemyWins;

            case Enemies.SALMONELLA:
                if (defense == Defenses.LINFOCITO_T)
                    return BattleState.EnemyWins;
                else if (defense == Defenses.LINFOCITO_B || defense == Defenses.NEUTROFILO)
                    return BattleState.DefenseWins;
                else
                    return BattleState.Neutral;

            case Enemies.HEPATITIS_B:
                if (defense == Defenses.LINFOCITO_B || defense == Defenses.BASOFILO)
                    return BattleState.EnemyWins;
                else if (defense == Defenses.MONOCITO || defense == Defenses.NEUTROFILO)
                    return BattleState.DefenseWins;
                else
                    return BattleState.Neutral;

            case Enemies.NISSERIA_GONORRHOEAE:
                if (defense == Defenses.NEUTROFILO)
                    return BattleState.EnemyWins;
                else if (defense == Defenses.EOSINOFILO)
                    return BattleState.DefenseWins;
                else
                    return BattleState.Neutral;

            case Enemies.HIV:
                if (defense == Defenses.NEUTROFILO/* || defense == Defenses.MONOCITO*/)
                    return BattleState.EnemyWins;
                else if (defense == Defenses.LINFOCITO_T)
                    return BattleState.DefenseWins;
                else
                    return BattleState.Neutral;

            case Enemies.INFLUENZA:
                if (defense == Defenses.MONOCITO)
                    return BattleState.EnemyWins;
                else if (defense == Defenses.BASOFILO)
                    return BattleState.DefenseWins;
                else
                    return BattleState.Neutral;

            default:
                return BattleState.Neutral;
        }
    }
}

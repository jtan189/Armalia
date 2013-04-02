using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Armalia.Characters
{
    abstract class EnemyCharacter
    {

        enum EnemyState
        {
            Patrol,
            Detection,
            Battle,
            Defeat
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace AvHModHelper.MonoBehaviors;

public class ShadowMonkey : MonoBehaviour
{
    static readonly EquipmentScript __instance = EquipmentScript.instance;
    
    static readonly List<GameObject> root = __instance.gameObject.scene.GetRootGameObjects().ToList();
    readonly GameObject player = root.Find(x => x.name == "FirstPersonCharacter");


    public void LateUpdate()
    {
        var position = player.transform.position;
        position.y = 0.5f;
        
        transform.position = position;
    }
}

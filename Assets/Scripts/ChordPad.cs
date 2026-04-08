using UnityEngine;

public class ChordPad : MonoBehaviour
{
    public SynthManager synthManager;
    public int chordIndex;

    public void ActivateChord()
    {
        synthManager.SetChord(chordIndex);
    }
}
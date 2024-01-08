using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SpellCasting
{
    public class SpellsBase : MonoBehaviour
    {
        private SpellData currentSpell;
        private SpellNamesEnum[] spellArray;
        private LimitedQueue<int> sigilList = new LimitedQueue<int>(2);
        private LimitedQueue<Sigil> sigilBallList = new LimitedQueue<Sigil>(2);
        private LimitedQueue<SpellData> invokedSpell1 = new LimitedQueue<SpellData>(0);
        private LimitedQueue<SpellData> invokedSpell2 = new LimitedQueue<SpellData>(0);
        private Transform currentLocation;
        private SpellCastController spellController;

        [SerializeField] private Sigil quas;
        [SerializeField] private Sigil wex;
        [SerializeField] private Sigil exort;
        [SerializeField] private List<SpellData> spellList = new List<SpellData>();

        private const int queueLimit = 3;
        // Start is called before the first frame update
        void Start()
        {
            GetAllSeplls();
            currentLocation = gameObject.transform;
            spellController = GetComponent<SpellCastController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (QueueSigilQ()) return;
            if (QueueSigilE()) return;
            if (QueueSigilW()) return;
            if (Invoke()) return;
            if (CastSpells()) return;
        }

        private bool CastSpells()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                try
                {
                    //TODO: add casting for spells
                    Cast(invokedSpell1.Peek());
                    Debug.Log(invokedSpell1.Peek());
                    return true;
                }
                catch(InvalidOperationException e)
                {
                    Debug.Log("No Spell queued");
                    return false;
                }
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                try
                {
                    //TODO: add casting for spells
                    Cast(invokedSpell2.Peek());
                    Debug.Log(invokedSpell2.Peek());
                    return true;
                }
                catch (InvalidOperationException e)
                {
                    Debug.Log("No Spell queued");
                    return false;
                }
            }
            return false;
        }

        private void Cast(SpellData _spell)
        {
            RaycastHit hit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out hit); //passing the empty hit variable because the method will give what it hit into the hit variable

            if (hasHit)
            {
                try
                {
                    //Doesn't work, if <100 must be on player, if >100 unlimited range
                    //if (Math.Abs(gameObject.transform.position.x - hit.transform.position.x) > _spell.castRange || Math.Abs(gameObject.transform.position.z - hit.transform.position.z) > _spell.castRange) throw new IndexOutOfRangeException();
                    spellController.SetSpellAndCast(_spell, hit.point, Quaternion.identity);
                }
                catch(NullReferenceException e)
                {
                    Debug.LogException(e);
                    Debug.Log("No spell queued");
                }
                catch(IndexOutOfRangeException ex)
                {
                    Debug.Log("Spell out of range");
                }
                //GameObject castSpell = Instantiate(_spell.spellPrefab, hit.point, Quaternion.identity);
                //Destroy(castSpell, 2f);
            }

            
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private SpellData GetSpellByID(int id)
        {
            return spellList[id];
        }

        private void GetAllSeplls()
        {
            spellArray = (SpellNamesEnum[])Enum.GetValues(typeof(SpellNamesEnum));
        }

        private bool QueueSigilQ()

        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                sigilList.LimitedEnqueue(0);
                Sigil newSigil = Instantiate(quas, currentLocation);
                Renderer rend = newSigil.GetComponentInChildren<Renderer>();
                rend.material.color = Color.blue;
                sigilBallList.LimitedEnqueue(newSigil);
                try
                {
                    Destroy(sigilBallList.recentDequeue.gameObject);
                }
                catch(NullReferenceException e)
                {
                    Debug.Log("Sigil Queue is empty");
                }
                return true;
            }
            return false;
        }


        private bool QueueSigilW()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                sigilList.LimitedEnqueue(1);
                Sigil newSigil = Instantiate(wex, currentLocation);
                Renderer rend = newSigil.GetComponentInChildren<Renderer>();
                rend.material.color = Color.magenta;
                sigilBallList.LimitedEnqueue(newSigil);
                try
                {
                    Destroy(sigilBallList.recentDequeue.gameObject);
                }
                catch (NullReferenceException e)
                {
                    Debug.Log("Sigil Queue is empty");
                }
                return true;
            }
            return false;
        }
        private bool QueueSigilE()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                sigilList.LimitedEnqueue(4);
                Sigil newSigil = Instantiate(exort, currentLocation);
                Renderer rend = newSigil.GetComponentInChildren<Renderer>();
                rend.material.color = Color.red;
                sigilBallList.LimitedEnqueue(newSigil);
                try
                {
                    Destroy(sigilBallList.recentDequeue.gameObject);
                }
                catch (NullReferenceException e)
                {
                    Debug.Log("Sigil Queue is empty");
                }
                return true;
            }
            return false;
        }
        private bool Invoke()
        {
            //TODO: Add unique Ids so that that same spell can't be queued twice
            int sumOfQueue = SumQueue();
            if (Input.GetKeyDown(KeyCode.R) && sumOfQueue > -1)
            {
                currentSpell = GetSpellByID(sumOfQueue);
                if(!invokedSpell1.IsEmpty())
                {
                    invokedSpell1.LimitedEnqueue(currentSpell);
                    invokedSpell2.LimitedEnqueue(invokedSpell1.recentDequeue);
                    Debug.Log(currentSpell.name);
                }
                else
                {
                    invokedSpell1.LimitedEnqueue(currentSpell);
                }
                return true;
            }
            return false;
        }

        private void DestroyRecentDequeue()
        {
            Destroy(sigilBallList.recentDequeue.gameObject);
        }

        private int SumQueue()
        {
            int sum = -1;
            if (sigilList.Count != 3) return sum;
            sum = 0;
            foreach (int num in sigilList)
            {
                sum += num;
            }
            return sum;

        }
    }
}
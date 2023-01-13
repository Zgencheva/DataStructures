namespace RoyaleArena
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Arena : IArena
    {
        public Arena()
        {
            this.BattleCards = new Dictionary<int, BattleCard>();
        }
        public Dictionary<int, BattleCard> BattleCards;
        public int Count => this.BattleCards.Count;

        public void Add(BattleCard card)
        {
            if (!this.BattleCards.ContainsKey(card.Id))
            {
                this.BattleCards.Add(card.Id, card);
            }
        }

        public void ChangeCardType(int id, CardType type)
        {
            if (!this.BattleCards.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }
            this.BattleCards[id].Type = type;
        }

        public bool Contains(BattleCard card)
        {
            return this.BattleCards.ContainsKey(card.Id);
        }

        public IEnumerable<BattleCard> FindFirstLeastSwag(int n)
        {
            if (n > this.Count)
            {
                throw new InvalidOperationException();
            }
            var result = this.BattleCards.Values
                .OrderBy(x => x.Swag)
                .ThenBy(x => x.Id)
                .Take(n);

            return result.ToList();
        }

        public IEnumerable<BattleCard> GetAllInSwagRange(double lo, double hi)
        {
            var result = this.BattleCards.Values
             .Where(x=> lo <= x.Swag && x.Swag <= hi)
             .OrderBy(x => x.Swag)
             .ThenBy(x => x.Id);
            return result.ToList();
        }

        public IEnumerable<BattleCard> GetByCardType(CardType type)
        {
            var result = this.BattleCards.Values.Where(x => x.Type == type)
                .OrderByDescending(x=> x.Damage)
                .ThenBy(x=>x.Id);
            if (!result.Any())
            {
                throw new InvalidOperationException();
            }
            return result.ToList();
        }

        public IEnumerable<BattleCard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
        {
            var result = this.BattleCards.Values
                .Where(x => x.Type == type && x.Damage <= damage)
                .OrderByDescending(x => x.Damage)
                .ThenBy(x => x.Id);
            if (!result.Any())
            {
                throw new InvalidOperationException();
            }
            return result.ToList();
        }

        public BattleCard GetById(int id)
        {
            if (!this.BattleCards.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }
            return this.BattleCards[id];
        }

        public IEnumerable<BattleCard> GetByNameAndSwagRange(string name, double lo, double hi)
        {
            var result = this.BattleCards.Values
              .Where(x => x.Name == name && lo <= x.Swag && x.Swag <= hi)
              .OrderByDescending(x => x.Swag)
              .ThenBy(x => x.Id);
            if (!result.Any())
            {
                throw new InvalidOperationException();
            }
            return result.ToList();
        }

        public IEnumerable<BattleCard> GetByNameOrderedBySwagDescending(string name)
        {
            var result = this.BattleCards.Values
               .Where(x => x.Name == name)
               .OrderByDescending(x => x.Swag)
               .ThenBy(x => x.Id);
            if (!result.Any())
            {
                throw new InvalidOperationException();
            }
            return result.ToList();
        }

        public IEnumerable<BattleCard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
        {
            var result = this.BattleCards.Values
               .Where(x => x.Type == type && lo <= x.Damage && x.Damage <= hi)
               .OrderByDescending(x => x.Damage)
               .ThenBy(x => x.Id);
            if (!result.Any())
            {
                throw new InvalidOperationException();
            }
            return result.ToList();
        }

        public void RemoveById(int id)
        {
            if (!this.BattleCards.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }
            this.BattleCards.Remove(id);
        }
        public IEnumerator<BattleCard> GetEnumerator()
        {
            foreach (var battleCard in BattleCards)
            {
                yield return battleCard.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
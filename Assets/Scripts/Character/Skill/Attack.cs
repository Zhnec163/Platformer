public class Attack : Skill
{
    protected override void Cast()
    {
        if (TryGetNearCharacter(out Character character))
            character.TakeDamage(Damage);            
    }
}
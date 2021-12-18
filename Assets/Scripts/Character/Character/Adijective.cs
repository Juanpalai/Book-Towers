using UnityEngine;

public class Adijective : Character
{
	private const int MAX_ATTACK_RANGE = 8;
	private void OnTriggerEnter( Collider other ) {
		if ( other.GetComponent<Character>( ).getCamp( ) == this.getCamp( ) ||
			_enemy_in_scope.Count == MAX_ATTACK_RANGE ) {
			return;
		}
		_enemy_in_scope.Add( other.GetComponent<Character>( ) );
	}
}

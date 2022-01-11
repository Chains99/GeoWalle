using System.Collections;
using System.Collections.Generic;

namespace GeoWallE.Parsing.LexicalAnalysis
{
    public class TokenConsumer : IEnumerable<Token>
    {
        private List<Token> _tokens;
        private int _position;
        public int Count () {  return _tokens.Count; } 

        public TokenConsumer(IEnumerable<Token> tokens)
        {
            _tokens = new List<Token>(tokens);
            _position = 0;
        }

        public bool EndOfTokens
        {
            get { return _position == _tokens.Count; }
        }
        public  void setPosition(int _position)
        {
            this._position = _position;
        
        }
        public int getPosition()
        {
            return this._position;
        }

        public Token Current
        {
            get { return _tokens[_position]; }
        }

        public bool Next()
        {
            if (_position < _tokens.Count)
            {
                _position++;
            }

            return _position < _tokens.Count;
        }
        public Token CurrentPrev
        {
            get { return _tokens[_position - 1]; }
        }

        public bool CanLookAhead(int k)
        {
            return _tokens.Count - _position > k;
        }

        public Token LookAhead(int k)
        {
            return _tokens[_position + k];
        }

        public IEnumerator<Token> GetEnumerator()
        {
            for (int i = _position; i < _tokens.Count; i++)
                yield return _tokens[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

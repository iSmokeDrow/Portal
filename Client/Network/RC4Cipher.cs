// Copyright (c) Tartarus Dev Team, licensed under GNU GPL.
// See the LICENSE file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Network
{
	// Adapted from http://www.superstarcoders.com/blogs/posts/rc4-encryption-in-c-sharp.aspx
	public class RC4Cipher
	{
		#region State Struct

		public struct State
		{
			public int x, y;
			public byte[] s;
		}

		#endregion

		public bool Init(string pKey)
		{
			return PrepareKey(pKey);
		}

		#region Encrypt and Decrypt Publics

		public byte[] Code(ref byte[] pSrc, int len)
		{
			return CodeBlock(ref pSrc, len);
		}

		public byte[] Encode(ref byte[] pSrc, int len)
		{
			return CodeBlock(ref pSrc, len);
		}

		public byte[] Decode(ref byte[] pSrc, int len)
		{
			return CodeBlock(ref pSrc, len);
		}

		#endregion

		#region Backup

		public void SaveStateTo(out State outState) { outState = m_state; }
		public void LoadStateFrom(ref State aState) { m_state = aState; }

		#endregion

		// Privates
		private State m_state;

		private bool PrepareKey(string pKey)
		{
			if (String.IsNullOrEmpty(pKey))
				return false;

			int keyLen = pKey.Length;

			m_state = new State();
			m_state.s = new byte[256];

			int i, j = 0;
			for (i = 0; i < 256; i++)
				m_state.s[i] = (byte)i;

			byte[] key = new byte[256];

			j = 0;
			for (i = 0; i < 256; i++)
			{
				key[i] = (byte) pKey[j++];
				if (j >= keyLen)
					j = 0;
			}

			j = 0;
			for (i = 0; i < 256; i++)
			{
				j = (j + m_state.s[j] + key[j]) & 0xFF;
				Swap(m_state.s, i, j);
			}

			m_state.x = m_state.y = 0;
			SkipFor(1013);

			return true;
		}

		private byte[] CodeBlock(ref byte[] pSrc, int len)
		{
            byte[] pDst = new byte[len];
			int x = m_state.x, y = m_state.y;

			for (int offset = 0; offset < len; offset++)
			{
				x = (x + 1) & 0xFF;
				int sx = m_state.s[x];
				y = (y + sx) & 0xFF;
				int sy = m_state.s[y];
				m_state.s[x] = (byte) sy; m_state.s[y] = (byte) sx;
				pDst[offset] = (byte)((int)pSrc[offset] ^ (int)m_state.s[((sx + sy) & 0xFF)]);
			}

			m_state.x = x;
			m_state.y = y;
            
            return pDst;
		}

		private IEnumerable<byte> EncryptOutput(IEnumerable<byte> data)
		{
			int i = 0;
			int j = 0;

			return data.Select((b) =>
			{
				i = (i + 1) & 0xFF;
				j = (j + m_state.s[i]) & 0xFF;

				Swap(m_state.s, i, j);

				return (byte)(b ^ m_state.s[(m_state.s[i] + m_state.s[j]) & 0xFF]);
			});
		}

		private void Swap(byte[] s, int i, int j)
		{
			byte c = s[i];

			s[i] = s[j];
			s[j] = c;
		}

		private void SkipFor(int len)
		{
			int x = 0; int y = 0;
			while (len-- > 0)
			{
				x = (x + 1) & 0xFF;
				int sx = m_state.s[x];
				y = (y + sx) & 0xFF;
				m_state.s[x] = m_state.s[y]; m_state.s[y] = (byte)sx;
			}

			m_state.x = x; m_state.y = y;
		}
	}
}

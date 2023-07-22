﻿using Sandbox.Game.Entities.Character;
using SpaceEngineersVR.Player.Components;
using System.Runtime.CompilerServices;
using Valve.VR;
using VRageMath;

namespace SpaceEngineersVR.Util
{
	internal static class Extensions
	{

		public static Vector3 ToVector(this HmdVector3_t v)
		{
			return new Vector3(v.v0, v.v1, v.v2);
		}

		public static Vector3D ToVector(this HmdVector3d_t v)
		{
			return new Vector3D(v.v0, v.v1, v.v2);
		}

		static unsafe void* GetObjectAddress(this object obj)
		{
			return *(void**)Unsafe.AsPointer(ref obj);
		}
		public static unsafe void TransmuteTo(this object target, object source)
		{
			if (target.GetType() == source.GetType())
				return;

			void** s = (void**)source.GetObjectAddress();
			void** t = (void**)target.GetObjectAddress();
			*t = *s;
		}

		public static VRMovementComponent VRMovement(this MyCharacter c)
		{
			return c.Components.Get<VRMovementComponent>();
		}
		public static VRBodyComponent VRBody(this MyCharacter c)
		{
			return c.Components.Get<VRBodyComponent>();
		}

		//Matrix
		//11 12 13 right
		//21 22 23 up
		//31 32 33 backward
		//41 42 43 translation

		//HmdMatrix34_t
		//0 4  8 right
		//1 5  9 up
		//2 6 10 backward
		//3 7 11 translation

		public static Matrix ToMatrix(this HmdMatrix34_t hmd)
		{
			return new Matrix(
				hmd.m0, hmd.m4, hmd.m8, 0f,
				hmd.m1, hmd.m5, hmd.m9, 0f,
				hmd.m2, hmd.m6, hmd.m10, 0f,
				hmd.m3, hmd.m7, hmd.m11, 1f);
		}

		public static Matrix ToMatrix(this HmdMatrix44_t hmd)
		{
			return new Matrix(
				hmd.m0, hmd.m4, hmd.m8, hmd.m12,
				hmd.m1, hmd.m5, hmd.m9, hmd.m13,
				hmd.m2, hmd.m6, hmd.m10, hmd.m14,
				hmd.m3, hmd.m7, hmd.m11, hmd.m15);
		}

		public static HmdMatrix34_t ToHMDMatrix34(this Matrix mat)
		{
			return new HmdMatrix34_t()
			{
				m0 = mat.M11, m4 = mat.M12, m8  = mat.M13,
				m1 = mat.M21, m5 = mat.M22, m9  = mat.M23,
				m2 = mat.M31, m6 = mat.M32, m10 = mat.M33,
				m3 = mat.M41, m7 = mat.M42, m11 = mat.M43
			};
		}

		public static HmdMatrix44_t ToHMDMatrix44(this Matrix mat)
		{
			return new HmdMatrix44_t()
			{
				m0 = mat.M11, m4 = mat.M12, m8  = mat.M13, m12 = mat.M14,
				m1 = mat.M21, m5 = mat.M22, m9  = mat.M23, m13 = mat.M24,
				m2 = mat.M31, m6 = mat.M32, m10 = mat.M33, m14 = mat.M34,
				m3 = mat.M43, m7 = mat.M42, m11 = mat.M43, m15 = mat.M44
			};
		}
	}
}

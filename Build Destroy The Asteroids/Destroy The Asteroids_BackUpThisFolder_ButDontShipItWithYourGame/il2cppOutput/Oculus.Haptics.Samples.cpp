#include "pch-cpp.hpp"





template <typename T1>
struct VirtualActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};

struct TweenRunner_1_t5BB0582F926E75E2FE795492679A6CF55A4B4BC4;
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct UIVertexU5BU5D_tBC532486B45D071A520751A90E819C77BA4E3D2F;
struct Vector2U5BU5D_tFEBBC94BCC6C9C88277BA04047D2B3FDB6ED7FDA;
struct Vector3U5BU5D_tFF1859CCE176131B909E2044F76443064254679C;
struct CancellationTokenSource_tAAE1E0033BCFC233801F8CB4CED5C852B350CB7B;
struct Canvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26;
struct CanvasRenderer_tAB9A55A976C4E3B2B37D0CE5616E5685A8B43860;
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2;
struct Exception_t;
struct FontData_tB8E562846C6CB59C43260F69AE346B9BF3157224;
struct HapticClip_tF8BCD1E73DCA942D4445DA6E85D0A3492D2799B6;
struct HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349;
struct Haptics_t93216D86CF0A99726A61DDED24C12CAFBACE5F7B;
struct HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D;
struct HapticsSdkOVRControllerGuidance_t855408C22FF69F5FE72ABD5FE60021852704023C;
struct HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct Material_t18053F08F347D0DCA5E1140EC7EC4533DD8A14E3;
struct Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4;
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71;
struct RectMask2D_tACF92BE999C791A665BD1ADEABF5BCEB82846670;
struct RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct String_t;
struct SynchronizationContext_tCDB842BBE53B050802CBBB59C6E6DC45B5B06DC0;
struct Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62;
struct TextGenerator_t85D00417640A53953556C01F9D4E7DDE1ABD8FEC;
struct Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4;
struct UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7;
struct UnitySourceGeneratedAssemblyMonoScriptTypes_v1_tB77C605CD8AC458B3B1B8099F527E54E3CA8523E;
struct VertexHelper_tB905FCB02AE67CBEE5F265FE37A5938FC5D136FE;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
struct CullStateChangedEvent_t6073CD0D951EC1256BF74B8F9107D68FC89B99B8;

IL2CPP_EXTERN_C RuntimeClass* ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Exception_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* OVRInput_t759D9CC8F14D8539AD78A0375A2306C39D77100A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeField* U3CPrivateImplementationDetailsU3E_tE8C75C91D58BEC9F762459030BB60F85C1184FEA____BB6DDED0FD805DD922372D0F899FCB2DDE7A59BCDBFEE8018B1C89B6C6216CA1_FieldInfo_var;
IL2CPP_EXTERN_C RuntimeField* U3CPrivateImplementationDetailsU3E_tE8C75C91D58BEC9F762459030BB60F85C1184FEA____FD9ACF6733B5D027865E9899C3D90D58A3AADA0076058460270A1BAB8B35F378_FieldInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral133479D57A19A97DAD0A52860E28734914D6BA65;
IL2CPP_EXTERN_C String_t* _stringLiteral1B9C0C3911790E1BCA24CDC494DBAB4BBB52437F;
IL2CPP_EXTERN_C String_t* _stringLiteral224132448A22B570EDBEBDD5A55436D7F15C8A43;
IL2CPP_EXTERN_C String_t* _stringLiteral372DD8EE3DD1BF0CC9F51C568C39B9466C6D48C8;
IL2CPP_EXTERN_C String_t* _stringLiteral46D551A851A702EF3CB05D5D664916D4996E850C;
IL2CPP_EXTERN_C String_t* _stringLiteral4CD9DCF1D6A03B7AF7C79C9DE5CE9D5DE6ACB217;
IL2CPP_EXTERN_C String_t* _stringLiteral4F476103F4E5CEABC0A44FD75F2E07C67AE64C66;
IL2CPP_EXTERN_C String_t* _stringLiteral63FA5D3F0CAA1ACBCFB620AD6C35566340B9B7FC;
IL2CPP_EXTERN_C String_t* _stringLiteral69969C56A91534E22881689E82CE3DA725CF0CF1;
IL2CPP_EXTERN_C String_t* _stringLiteral77330E8247C65E2C9DD49AF22EAC02018A75011A;
IL2CPP_EXTERN_C String_t* _stringLiteral7EC332E8BA6512A0EF453FA1305DC5428FCDE3EF;
IL2CPP_EXTERN_C String_t* _stringLiteralA65111E1AFCDCA0D36968D39E3597AE7538F6A27;
IL2CPP_EXTERN_C String_t* _stringLiteralD2D1EA4A29191701858D25C9DCBE68BFD96CBF29;
IL2CPP_EXTERN_C String_t* _stringLiteralE3CAC917A9440E06AF4AB8FE6B319C474FA111EF;
IL2CPP_EXTERN_C String_t* _stringLiteralF3E84B722399601AD7E281754E917478AA9AD48D;
IL2CPP_EXTERN_C String_t* _stringLiteralFE85B285A9E71521E4E2B92C2F355FAC2744CACC;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_tD32C9E1FE76BCCBA607B796FF48D6C60231E02AB 
{
};
struct U3CPrivateImplementationDetailsU3E_tE8C75C91D58BEC9F762459030BB60F85C1184FEA  : public RuntimeObject
{
};
struct HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349  : public RuntimeObject
{
	int32_t ____clipId;
	int32_t ____playerId;
	Haptics_t93216D86CF0A99726A61DDED24C12CAFBACE5F7B* ____haptics;
};
struct Haptics_t93216D86CF0A99726A61DDED24C12CAFBACE5F7B  : public RuntimeObject
{
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct UnitySourceGeneratedAssemblyMonoScriptTypes_v1_tB77C605CD8AC458B3B1B8099F527E54E3CA8523E  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3 
{
	uint8_t ___m_value;
};
struct Color_tD001788D726C3A7F1379BEED0260B9591F440C1F 
{
	float ___r;
	float ___g;
	float ___b;
	float ___a;
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2  : public ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_pinvoke
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_com
{
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	float ___m_value;
};
struct UInt32_t1833D51FFA667B18A5AA4B8D34DE284F8495D29B 
{
	uint32_t ___m_value;
};
struct Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 
{
	float ___x;
	float ___y;
};
struct Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 
{
	float ___x;
	float ___y;
	float ___z;
	float ___w;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
#pragma pack(push, tp, 1)
struct __StaticArrayInitTypeSizeU3D351_tB8727C7F535E22204AABC5031C20779BB241AC62 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D351_tB8727C7F535E22204AABC5031C20779BB241AC62__padding[351];
	};
};
#pragma pack(pop, tp)
#pragma pack(push, tp, 1)
struct __StaticArrayInitTypeSizeU3D87_t791F10D56F05303E3F9E468539D6B30F88C9D947 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D87_t791F10D56F05303E3F9E468539D6B30F88C9D947__padding[87];
	};
};
#pragma pack(pop, tp)
struct MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B 
{
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___FilePathsData;
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___TypesData;
	int32_t ___TotalTypes;
	int32_t ___TotalFiles;
	bool ___IsEditorOnly;
};
struct MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B_marshaled_pinvoke
{
	Il2CppSafeArray* ___FilePathsData;
	Il2CppSafeArray* ___TypesData;
	int32_t ___TotalTypes;
	int32_t ___TotalFiles;
	int32_t ___IsEditorOnly;
};
struct MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B_marshaled_com
{
	Il2CppSafeArray* ___FilePathsData;
	Il2CppSafeArray* ___TypesData;
	int32_t ___TotalTypes;
	int32_t ___TotalFiles;
	int32_t ___IsEditorOnly;
};
struct Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D 
{
	int32_t ___value__;
};
struct Exception_t  : public RuntimeObject
{
	String_t* ____className;
	String_t* ____message;
	RuntimeObject* ____data;
	Exception_t* ____innerException;
	String_t* ____helpURL;
	RuntimeObject* ____stackTrace;
	String_t* ____stackTraceString;
	String_t* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	RuntimeObject* ____dynamicMethods;
	int32_t ____HResult;
	String_t* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_pinvoke
{
	char* ____className;
	char* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_pinvoke* ____innerException;
	char* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	char* ____stackTraceString;
	char* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	char* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className;
	Il2CppChar* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_com* ____innerException;
	Il2CppChar* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	Il2CppChar* ____stackTraceString;
	Il2CppChar* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	Il2CppChar* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr;
};
struct RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 
{
	intptr_t ___value;
};
struct Button_tA5F6C329BA19678946F9C1B5827CA5AE3E48A5FB 
{
	int32_t ___value__;
};
struct Controller_tBCAA59EAD9BFE2381E974A48B203D044B6CE1ECB 
{
	int32_t ___value__;
};
struct RawAxis2D_t882F928A0CB13BA12DBC6402B64ECA76202DFBA1 
{
	int32_t ___value__;
};
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A_marshaled_pinvoke : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
};
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A_marshaled_com : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
};
struct Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};
struct HapticClip_tF8BCD1E73DCA942D4445DA6E85D0A3492D2799B6  : public ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A
{
	String_t* ___json;
};
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
	CancellationTokenSource_tAAE1E0033BCFC233801F8CB4CED5C852B350CB7B* ___m_CancellationTokenSource;
};
struct HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62* ___popUpText;
};
struct HapticsSdkOVRControllerGuidance_t855408C22FF69F5FE72ABD5FE60021852704023C  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D* ___hapticsSdkGuidance;
	HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* ___hapticsSdkPlaySample;
};
struct HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	HapticClip_tF8BCD1E73DCA942D4445DA6E85D0A3492D2799B6* ___clip1;
	HapticClip_tF8BCD1E73DCA942D4445DA6E85D0A3492D2799B6* ___clip2;
	HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* ___leftClipPlayer1;
	HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* ___leftClipPlayer2;
	HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* ___rightClipPlayer1;
	HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* ___rightClipPlayer2;
};
struct UIBehaviour_tB9D4295827BD2EEDEF0749200C6CA7090C742A9D  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
};
struct Graphic_tCBFCA4585A19E2B75465AECFEAC43F4016BF7931  : public UIBehaviour_tB9D4295827BD2EEDEF0749200C6CA7090C742A9D
{
	Material_t18053F08F347D0DCA5E1140EC7EC4533DD8A14E3* ___m_Material;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___m_Color;
	bool ___m_SkipLayoutUpdate;
	bool ___m_SkipMaterialUpdate;
	bool ___m_RaycastTarget;
	bool ___m_RaycastTargetCache;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___m_RaycastPadding;
	RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5* ___m_RectTransform;
	CanvasRenderer_tAB9A55A976C4E3B2B37D0CE5616E5685A8B43860* ___m_CanvasRenderer;
	Canvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26* ___m_Canvas;
	bool ___m_VertsDirty;
	bool ___m_MaterialDirty;
	UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7* ___m_OnDirtyLayoutCallback;
	UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7* ___m_OnDirtyVertsCallback;
	UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7* ___m_OnDirtyMaterialCallback;
	Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4* ___m_CachedMesh;
	Vector2U5BU5D_tFEBBC94BCC6C9C88277BA04047D2B3FDB6ED7FDA* ___m_CachedUvs;
	TweenRunner_1_t5BB0582F926E75E2FE795492679A6CF55A4B4BC4* ___m_ColorTweenRunner;
	bool ___U3CuseLegacyMeshGenerationU3Ek__BackingField;
};
struct MaskableGraphic_tFC5B6BE351C90DE53744DF2A70940242774B361E  : public Graphic_tCBFCA4585A19E2B75465AECFEAC43F4016BF7931
{
	bool ___m_ShouldRecalculateStencil;
	Material_t18053F08F347D0DCA5E1140EC7EC4533DD8A14E3* ___m_MaskMaterial;
	RectMask2D_tACF92BE999C791A665BD1ADEABF5BCEB82846670* ___m_ParentMask;
	bool ___m_Maskable;
	bool ___m_IsMaskingGraphic;
	bool ___m_IncludeForMasking;
	CullStateChangedEvent_t6073CD0D951EC1256BF74B8F9107D68FC89B99B8* ___m_OnCullStateChanged;
	bool ___m_ShouldRecalculate;
	int32_t ___m_StencilValue;
	Vector3U5BU5D_tFF1859CCE176131B909E2044F76443064254679C* ___m_Corners;
};
struct Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62  : public MaskableGraphic_tFC5B6BE351C90DE53744DF2A70940242774B361E
{
	FontData_tB8E562846C6CB59C43260F69AE346B9BF3157224* ___m_FontData;
	String_t* ___m_Text;
	TextGenerator_t85D00417640A53953556C01F9D4E7DDE1ABD8FEC* ___m_TextCache;
	TextGenerator_t85D00417640A53953556C01F9D4E7DDE1ABD8FEC* ___m_TextCacheForLayout;
	bool ___m_DisableFontTextureRebuiltCallback;
	UIVertexU5BU5D_tBC532486B45D071A520751A90E819C77BA4E3D2F* ___m_TempVerts;
};
struct U3CPrivateImplementationDetailsU3E_tE8C75C91D58BEC9F762459030BB60F85C1184FEA_StaticFields
{
	__StaticArrayInitTypeSizeU3D87_t791F10D56F05303E3F9E468539D6B30F88C9D947 ___BB6DDED0FD805DD922372D0F899FCB2DDE7A59BCDBFEE8018B1C89B6C6216CA1;
	__StaticArrayInitTypeSizeU3D351_tB8727C7F535E22204AABC5031C20779BB241AC62 ___FD9ACF6733B5D027865E9899C3D90D58A3AADA0076058460270A1BAB8B35F378;
};
struct Haptics_t93216D86CF0A99726A61DDED24C12CAFBACE5F7B_StaticFields
{
	Haptics_t93216D86CF0A99726A61DDED24C12CAFBACE5F7B* ___instance;
	SynchronizationContext_tCDB842BBE53B050802CBBB59C6E6DC45B5B06DC0* ___syncContext;
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_StaticFields
{
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___enumSeperatorCharArray;
};
struct Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7_StaticFields
{
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___zeroVector;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___oneVector;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___upVector;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___downVector;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___leftVector;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___rightVector;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___positiveInfinityVector;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___negativeInfinityVector;
};
struct Exception_t_StaticFields
{
	RuntimeObject* ___s_EDILock;
};
struct HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D_StaticFields
{
	int32_t ___U3CCurrentPopUpIndexU3Ek__BackingField;
};
struct Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62_StaticFields
{
	Material_t18053F08F347D0DCA5E1140EC7EC4533DD8A14E3* ___s_DefaultText;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031  : public RuntimeArray
{
	ALIGN_FIELD (8) uint8_t m_Items[1];

	inline uint8_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline uint8_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, uint8_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline uint8_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline uint8_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, uint8_t value)
	{
		m_Items[index] = value;
	}
};



IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void HapticsSdkGuidance_set_CurrentPopUpIndex_m7C8E4956FB861834E3CD2368DC6EB4D6AFF0D3B5_inline (int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t HapticsSdkGuidance_get_CurrentPopUpIndex_mF6127869105DFDC1A16952E34F0AD1148DF4D02A_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Int32_ToString_m030E01C24E294D6762FB0B6F37CB541581F55CA5 (int32_t* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B (String_t* ___0_str0, String_t* ___1_str1, String_t* ___2_str2, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9 (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E (MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkOVRControllerGuidance_HandleHapticsSdkGuidanceStep_m990DA130B4DCDDDC2C9830187EDD06D81C229DE4 (HapticsSdkOVRControllerGuidance_t855408C22FF69F5FE72ABD5FE60021852704023C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkOVRControllerGuidance_HandleControllerInput_m5E697F3394BD84F5254E3E0CC56D13A7CA5191DD (HapticsSdkOVRControllerGuidance_t855408C22FF69F5FE72ABD5FE60021852704023C* __this, int32_t ___0_controller, int32_t ___1_hand, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool OVRInput_GetUp_m66B13613FF16CBAB8B0A77A5ADCFD1A3A68F3898 (int32_t ___0_virtualMask, int32_t ___1_controllerMask, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkGuidance_NextStep_m5508198157C330A7FC145CAB20476AF27CDE8119 (HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 OVRInput_Get_mB457003E7F3A6A8901B7A1D6CB6A167A5829E304 (int32_t ___0_rawMask, int32_t ___1_controllerMask, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool OVRInput_GetDown_mEC4F71AEC93D3AF1A041934CA4C61680C6DB9AC7 (int32_t ___0_virtualMask, int32_t ___1_controllerMask, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkPlaySample_PlayFirstClip_mED4C03B8733C3C3B4B5794D69FE39DF35F488DD7 (HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* __this, int32_t ___0_hand, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkPlaySample_PlaySecondClip_m5E3830B68B705FDDCF75EB75161AD24084FEEF12 (HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* __this, int32_t ___0_hand, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkPlaySample_StopFirstClip_m5AD6B3AB085585458545BB2797EE2E38FEEDD44A (HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* __this, int32_t ___0_hand, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkPlaySample_StopSecondClip_mADBEE7F665F7FF3617EAC4B7B3EAA24FC337A500 (HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* __this, int32_t ___0_hand, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkPlaySample_SetLoopingOfFirstClip_m79080372E878BA176F3DF8694AC70204DED920FB (HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* __this, int32_t ___0_hand, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Clamp_mEB9AEA827D27D20FCC787F7375156AF46BB12BBF_inline (float ___0_value, float ___1_min, float ___2_max, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Vector2__ctor_m9525B79969AFFE3254B303A40997A56DEEB6F548_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* __this, float ___0_x, float ___1_y, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkPlaySample_ModulateAmplitudeAndFrequencyOfFirstClip_m0B2D91A817207E188FA2154BCC4595689603ED51 (HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* __this, int32_t ___0_hand, Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___1_input, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2 (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticClipPlayer__ctor_m693F0978BAEA05D8F38243C88511E1F38AE068DA (HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* __this, HapticClip_tF8BCD1E73DCA942D4445DA6E85D0A3492D2799B6* ___0_clip, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticClipPlayer_set_priority_m5F11CFACD6FF93D5B7628C090613E4143C9BA434 (HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* __this, uint32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticClipPlayer_Play_mA82A3F90DF9A99942EA2BC41874BAF638E90B644 (HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* __this, int32_t ___0_controller, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741 (RuntimeObject* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m9E3155FB84015C823606188F53B47CB44C444991 (String_t* ___0_str0, String_t* ___1_str1, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticClipPlayer_Stop_m110F3977766D9399363EB9E06982023253AC9B19 (HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool HapticClipPlayer_get_isLooping_m1A05073308EA34D9B8F96980993C33BD10A020E3 (HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticClipPlayer_set_isLooping_mECB0B0F5E896B2C12E00EB4238E1BA821A6535C5 (HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* __this, bool ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_mA8DBB4C2516B9723C5A41E6CB1E2FAF4BBE96DD8 (String_t* ___0_format, RuntimeObject* ___1_arg0, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticClipPlayer_set_amplitude_mBDD32378F8338E8611D8D57AF9690129BBEB8CBD (HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* __this, float ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticClipPlayer_set_frequencyShift_m1787B7594F4E870727FDB3EEBED1FA5C5DE7EDF0 (HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* __this, float ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticClipPlayer_Dispose_m64F1567B14DE1504A97535361A6EA9838266527E (HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Haptics_t93216D86CF0A99726A61DDED24C12CAFBACE5F7B* Haptics_get_Instance_m949CEF3AA5651088AC4B3991580137E0EAA1D819 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Haptics_Dispose_mB478AF6745C494BF898DCFBA07E593635F9FBAAB (Haptics_t93216D86CF0A99726A61DDED24C12CAFBACE5F7B* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RuntimeHelpers_InitializeArray_m751372AA3F24FBF6DA9B9D687CBFA2DE436CAB9B (RuntimeArray* ___0_array, RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 ___1_fldHandle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t HapticsSdkGuidance_get_CurrentPopUpIndex_mF6127869105DFDC1A16952E34F0AD1148DF4D02A (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		int32_t L_0 = ((HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D_StaticFields*)il2cpp_codegen_static_fields_for(HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D_il2cpp_TypeInfo_var))->___U3CCurrentPopUpIndexU3Ek__BackingField;
		return L_0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkGuidance_set_CurrentPopUpIndex_m7C8E4956FB861834E3CD2368DC6EB4D6AFF0D3B5 (int32_t ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		int32_t L_0 = ___0_value;
		((HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D_StaticFields*)il2cpp_codegen_static_fields_for(HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D_il2cpp_TypeInfo_var))->___U3CCurrentPopUpIndexU3Ek__BackingField = L_0;
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkGuidance_Awake_m8AEE3EF9F183D9E22F4B4067A9D4BEAC4164F385 (HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral1B9C0C3911790E1BCA24CDC494DBAB4BBB52437F);
		s_Il2CppMethodInitialized = true;
	}
	{
		HapticsSdkGuidance_set_CurrentPopUpIndex_m7C8E4956FB861834E3CD2368DC6EB4D6AFF0D3B5_inline(0, NULL);
		Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62* L_0 = __this->___popUpText;
		NullCheck(L_0);
		VirtualActionInvoker1< String_t* >::Invoke(75, L_0, _stringLiteral1B9C0C3911790E1BCA24CDC494DBAB4BBB52437F);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkGuidance_NextStep_m5508198157C330A7FC145CAB20476AF27CDE8119 (HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral224132448A22B570EDBEBDD5A55436D7F15C8A43);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral4CD9DCF1D6A03B7AF7C79C9DE5CE9D5DE6ACB217);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral4F476103F4E5CEABC0A44FD75F2E07C67AE64C66);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral63FA5D3F0CAA1ACBCFB620AD6C35566340B9B7FC);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral77330E8247C65E2C9DD49AF22EAC02018A75011A);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7EC332E8BA6512A0EF453FA1305DC5428FCDE3EF);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralD2D1EA4A29191701858D25C9DCBE68BFD96CBF29);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	int32_t V_2 = 0;
	{
		int32_t L_0;
		L_0 = HapticsSdkGuidance_get_CurrentPopUpIndex_mF6127869105DFDC1A16952E34F0AD1148DF4D02A_inline(NULL);
		HapticsSdkGuidance_set_CurrentPopUpIndex_m7C8E4956FB861834E3CD2368DC6EB4D6AFF0D3B5_inline(((int32_t)il2cpp_codegen_add(L_0, 1)), NULL);
		int32_t L_1;
		L_1 = HapticsSdkGuidance_get_CurrentPopUpIndex_mF6127869105DFDC1A16952E34F0AD1148DF4D02A_inline(NULL);
		V_1 = L_1;
		int32_t L_2 = V_1;
		V_0 = L_2;
		int32_t L_3 = V_0;
		switch (((int32_t)il2cpp_codegen_subtract(L_3, 1)))
		{
			case 0:
			{
				goto IL_0034;
			}
			case 1:
			{
				goto IL_0047;
			}
			case 2:
			{
				goto IL_005a;
			}
			case 3:
			{
				goto IL_006d;
			}
			case 4:
			{
				goto IL_0080;
			}
		}
	}
	{
		goto IL_0093;
	}

IL_0034:
	{
		Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62* L_4 = __this->___popUpText;
		NullCheck(L_4);
		VirtualActionInvoker1< String_t* >::Invoke(75, L_4, _stringLiteral4F476103F4E5CEABC0A44FD75F2E07C67AE64C66);
		goto IL_00b7;
	}

IL_0047:
	{
		Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62* L_5 = __this->___popUpText;
		NullCheck(L_5);
		VirtualActionInvoker1< String_t* >::Invoke(75, L_5, _stringLiteral7EC332E8BA6512A0EF453FA1305DC5428FCDE3EF);
		goto IL_00b7;
	}

IL_005a:
	{
		Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62* L_6 = __this->___popUpText;
		NullCheck(L_6);
		VirtualActionInvoker1< String_t* >::Invoke(75, L_6, _stringLiteral77330E8247C65E2C9DD49AF22EAC02018A75011A);
		goto IL_00b7;
	}

IL_006d:
	{
		Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62* L_7 = __this->___popUpText;
		NullCheck(L_7);
		VirtualActionInvoker1< String_t* >::Invoke(75, L_7, _stringLiteralD2D1EA4A29191701858D25C9DCBE68BFD96CBF29);
		goto IL_00b7;
	}

IL_0080:
	{
		Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62* L_8 = __this->___popUpText;
		NullCheck(L_8);
		VirtualActionInvoker1< String_t* >::Invoke(75, L_8, _stringLiteral224132448A22B570EDBEBDD5A55436D7F15C8A43);
		goto IL_00b7;
	}

IL_0093:
	{
		int32_t L_9;
		L_9 = HapticsSdkGuidance_get_CurrentPopUpIndex_mF6127869105DFDC1A16952E34F0AD1148DF4D02A_inline(NULL);
		V_2 = L_9;
		String_t* L_10;
		L_10 = Int32_ToString_m030E01C24E294D6762FB0B6F37CB541581F55CA5((&V_2), NULL);
		String_t* L_11;
		L_11 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(_stringLiteral63FA5D3F0CAA1ACBCFB620AD6C35566340B9B7FC, L_10, _stringLiteral4CD9DCF1D6A03B7AF7C79C9DE5CE9D5DE6ACB217, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(L_11, NULL);
		goto IL_00b7;
	}

IL_00b7:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkGuidance__ctor_m67226127BB1C6A0F5AF8AD6BC5C8E566900422B1 (HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D* __this, const RuntimeMethod* method) 
{
	{
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkOVRControllerGuidance_Update_m23B7FF8FF4781DED7824A4D6B5358C97F61F1ED4 (HapticsSdkOVRControllerGuidance_t855408C22FF69F5FE72ABD5FE60021852704023C* __this, const RuntimeMethod* method) 
{
	{
		HapticsSdkOVRControllerGuidance_HandleHapticsSdkGuidanceStep_m990DA130B4DCDDDC2C9830187EDD06D81C229DE4(__this, NULL);
		HapticsSdkOVRControllerGuidance_HandleControllerInput_m5E697F3394BD84F5254E3E0CC56D13A7CA5191DD(__this, 1, 0, NULL);
		HapticsSdkOVRControllerGuidance_HandleControllerInput_m5E697F3394BD84F5254E3E0CC56D13A7CA5191DD(__this, 2, 1, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkOVRControllerGuidance_HandleHapticsSdkGuidanceStep_m990DA130B4DCDDDC2C9830187EDD06D81C229DE4 (HapticsSdkOVRControllerGuidance_t855408C22FF69F5FE72ABD5FE60021852704023C* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&OVRInput_t759D9CC8F14D8539AD78A0375A2306C39D77100A_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	bool V_2 = false;
	bool V_3 = false;
	bool V_4 = false;
	int32_t G_B5_0 = 0;
	int32_t G_B11_0 = 0;
	int32_t G_B17_0 = 0;
	{
		int32_t L_0;
		L_0 = HapticsSdkGuidance_get_CurrentPopUpIndex_mF6127869105DFDC1A16952E34F0AD1148DF4D02A_inline(NULL);
		V_1 = L_0;
		int32_t L_1 = V_1;
		V_0 = L_1;
		int32_t L_2 = V_0;
		switch (L_2)
		{
			case 0:
			{
				goto IL_0028;
			}
			case 1:
			{
				goto IL_0028;
			}
			case 2:
			{
				goto IL_0057;
			}
			case 3:
			{
				goto IL_007e;
			}
			case 4:
			{
				goto IL_0028;
			}
		}
	}
	{
		goto IL_00d2;
	}

IL_0028:
	{
		il2cpp_codegen_runtime_class_init_inline(OVRInput_t759D9CC8F14D8539AD78A0375A2306C39D77100A_il2cpp_TypeInfo_var);
		bool L_3;
		L_3 = OVRInput_GetUp_m66B13613FF16CBAB8B0A77A5ADCFD1A3A68F3898(((int32_t)16384), 1, NULL);
		if (L_3)
		{
			goto IL_0042;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(OVRInput_t759D9CC8F14D8539AD78A0375A2306C39D77100A_il2cpp_TypeInfo_var);
		bool L_4;
		L_4 = OVRInput_GetUp_m66B13613FF16CBAB8B0A77A5ADCFD1A3A68F3898(((int32_t)16384), 2, NULL);
		G_B5_0 = ((int32_t)(L_4));
		goto IL_0043;
	}

IL_0042:
	{
		G_B5_0 = 1;
	}

IL_0043:
	{
		V_2 = (bool)G_B5_0;
		bool L_5 = V_2;
		if (!L_5)
		{
			goto IL_0055;
		}
	}
	{
		HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D* L_6 = __this->___hapticsSdkGuidance;
		NullCheck(L_6);
		HapticsSdkGuidance_NextStep_m5508198157C330A7FC145CAB20476AF27CDE8119(L_6, NULL);
	}

IL_0055:
	{
		goto IL_00d4;
	}

IL_0057:
	{
		il2cpp_codegen_runtime_class_init_inline(OVRInput_t759D9CC8F14D8539AD78A0375A2306C39D77100A_il2cpp_TypeInfo_var);
		bool L_7;
		L_7 = OVRInput_GetUp_m66B13613FF16CBAB8B0A77A5ADCFD1A3A68F3898(2, 1, NULL);
		if (L_7)
		{
			goto IL_0069;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(OVRInput_t759D9CC8F14D8539AD78A0375A2306C39D77100A_il2cpp_TypeInfo_var);
		bool L_8;
		L_8 = OVRInput_GetUp_m66B13613FF16CBAB8B0A77A5ADCFD1A3A68F3898(2, 2, NULL);
		G_B11_0 = ((int32_t)(L_8));
		goto IL_006a;
	}

IL_0069:
	{
		G_B11_0 = 1;
	}

IL_006a:
	{
		V_3 = (bool)G_B11_0;
		bool L_9 = V_3;
		if (!L_9)
		{
			goto IL_007c;
		}
	}
	{
		HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D* L_10 = __this->___hapticsSdkGuidance;
		NullCheck(L_10);
		HapticsSdkGuidance_NextStep_m5508198157C330A7FC145CAB20476AF27CDE8119(L_10, NULL);
	}

IL_007c:
	{
		goto IL_00d4;
	}

IL_007e:
	{
		il2cpp_codegen_runtime_class_init_inline(OVRInput_t759D9CC8F14D8539AD78A0375A2306C39D77100A_il2cpp_TypeInfo_var);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_11;
		L_11 = OVRInput_Get_mB457003E7F3A6A8901B7A1D6CB6A167A5829E304(1, ((int32_t)-2147483648LL), NULL);
		float L_12 = L_11.___y;
		if ((!(((double)((double)L_12)) == ((double)(0.0)))))
		{
			goto IL_00bb;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(OVRInput_t759D9CC8F14D8539AD78A0375A2306C39D77100A_il2cpp_TypeInfo_var);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_13;
		L_13 = OVRInput_Get_mB457003E7F3A6A8901B7A1D6CB6A167A5829E304(2, ((int32_t)-2147483648LL), NULL);
		float L_14 = L_13.___y;
		G_B17_0 = ((((int32_t)((((double)((double)L_14)) == ((double)(0.0)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		goto IL_00bc;
	}

IL_00bb:
	{
		G_B17_0 = 1;
	}

IL_00bc:
	{
		V_4 = (bool)G_B17_0;
		bool L_15 = V_4;
		if (!L_15)
		{
			goto IL_00d0;
		}
	}
	{
		HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D* L_16 = __this->___hapticsSdkGuidance;
		NullCheck(L_16);
		HapticsSdkGuidance_NextStep_m5508198157C330A7FC145CAB20476AF27CDE8119(L_16, NULL);
	}

IL_00d0:
	{
		goto IL_00d4;
	}

IL_00d2:
	{
		goto IL_00d4;
	}

IL_00d4:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkOVRControllerGuidance_HandleControllerInput_m5E697F3394BD84F5254E3E0CC56D13A7CA5191DD (HapticsSdkOVRControllerGuidance_t855408C22FF69F5FE72ABD5FE60021852704023C* __this, int32_t ___0_controller, int32_t ___1_hand, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&OVRInput_t759D9CC8F14D8539AD78A0375A2306C39D77100A_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	bool V_1 = false;
	bool V_2 = false;
	bool V_3 = false;
	bool V_4 = false;
	bool V_5 = false;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 V_6;
	memset((&V_6), 0, sizeof(V_6));
	bool V_7 = false;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 V_8;
	memset((&V_8), 0, sizeof(V_8));
	Exception_t* V_9 = NULL;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
	}
	try
	{
		{
			int32_t L_0 = ___0_controller;
			il2cpp_codegen_runtime_class_init_inline(OVRInput_t759D9CC8F14D8539AD78A0375A2306C39D77100A_il2cpp_TypeInfo_var);
			bool L_1;
			L_1 = OVRInput_GetDown_mEC4F71AEC93D3AF1A041934CA4C61680C6DB9AC7(((int32_t)8192), L_0, NULL);
			V_0 = L_1;
			bool L_2 = V_0;
			if (!L_2)
			{
				goto IL_0020_1;
			}
		}
		{
			HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* L_3 = __this->___hapticsSdkPlaySample;
			int32_t L_4 = ___1_hand;
			NullCheck(L_3);
			HapticsSdkPlaySample_PlayFirstClip_mED4C03B8733C3C3B4B5794D69FE39DF35F488DD7(L_3, L_4, NULL);
		}

IL_0020_1:
		{
			int32_t L_5 = ___0_controller;
			il2cpp_codegen_runtime_class_init_inline(OVRInput_t759D9CC8F14D8539AD78A0375A2306C39D77100A_il2cpp_TypeInfo_var);
			bool L_6;
			L_6 = OVRInput_GetDown_mEC4F71AEC93D3AF1A041934CA4C61680C6DB9AC7(((int32_t)16384), L_5, NULL);
			V_1 = L_6;
			bool L_7 = V_1;
			if (!L_7)
			{
				goto IL_003e_1;
			}
		}
		{
			HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* L_8 = __this->___hapticsSdkPlaySample;
			int32_t L_9 = ___1_hand;
			NullCheck(L_8);
			HapticsSdkPlaySample_PlaySecondClip_m5E3830B68B705FDDCF75EB75161AD24084FEEF12(L_8, L_9, NULL);
		}

IL_003e_1:
		{
			int32_t L_10 = ___0_controller;
			il2cpp_codegen_runtime_class_init_inline(OVRInput_t759D9CC8F14D8539AD78A0375A2306C39D77100A_il2cpp_TypeInfo_var);
			bool L_11;
			L_11 = OVRInput_GetUp_m66B13613FF16CBAB8B0A77A5ADCFD1A3A68F3898(((int32_t)8192), L_10, NULL);
			V_2 = L_11;
			bool L_12 = V_2;
			if (!L_12)
			{
				goto IL_005c_1;
			}
		}
		{
			HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* L_13 = __this->___hapticsSdkPlaySample;
			int32_t L_14 = ___1_hand;
			NullCheck(L_13);
			HapticsSdkPlaySample_StopFirstClip_m5AD6B3AB085585458545BB2797EE2E38FEEDD44A(L_13, L_14, NULL);
		}

IL_005c_1:
		{
			int32_t L_15 = ___0_controller;
			il2cpp_codegen_runtime_class_init_inline(OVRInput_t759D9CC8F14D8539AD78A0375A2306C39D77100A_il2cpp_TypeInfo_var);
			bool L_16;
			L_16 = OVRInput_GetUp_m66B13613FF16CBAB8B0A77A5ADCFD1A3A68F3898(((int32_t)16384), L_15, NULL);
			V_3 = L_16;
			bool L_17 = V_3;
			if (!L_17)
			{
				goto IL_007a_1;
			}
		}
		{
			HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* L_18 = __this->___hapticsSdkPlaySample;
			int32_t L_19 = ___1_hand;
			NullCheck(L_18);
			HapticsSdkPlaySample_StopSecondClip_mADBEE7F665F7FF3617EAC4B7B3EAA24FC337A500(L_18, L_19, NULL);
		}

IL_007a_1:
		{
			int32_t L_20 = ___0_controller;
			il2cpp_codegen_runtime_class_init_inline(OVRInput_t759D9CC8F14D8539AD78A0375A2306C39D77100A_il2cpp_TypeInfo_var);
			bool L_21;
			L_21 = OVRInput_GetDown_mEC4F71AEC93D3AF1A041934CA4C61680C6DB9AC7(2, L_20, NULL);
			V_4 = L_21;
			bool L_22 = V_4;
			if (!L_22)
			{
				goto IL_0096_1;
			}
		}
		{
			HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* L_23 = __this->___hapticsSdkPlaySample;
			int32_t L_24 = ___1_hand;
			NullCheck(L_23);
			HapticsSdkPlaySample_SetLoopingOfFirstClip_m79080372E878BA176F3DF8694AC70204DED920FB(L_23, L_24, NULL);
		}

IL_0096_1:
		{
			int32_t L_25 = ___0_controller;
			V_5 = (bool)((((int32_t)L_25) == ((int32_t)1))? 1 : 0);
			bool L_26 = V_5;
			if (!L_26)
			{
				goto IL_00ef_1;
			}
		}
		{
			il2cpp_codegen_runtime_class_init_inline(OVRInput_t759D9CC8F14D8539AD78A0375A2306C39D77100A_il2cpp_TypeInfo_var);
			Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_27;
			L_27 = OVRInput_Get_mB457003E7F3A6A8901B7A1D6CB6A167A5829E304(1, ((int32_t)-2147483648LL), NULL);
			float L_28 = L_27.___x;
			Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_29;
			L_29 = OVRInput_Get_mB457003E7F3A6A8901B7A1D6CB6A167A5829E304(1, ((int32_t)-2147483648LL), NULL);
			float L_30 = L_29.___y;
			float L_31;
			L_31 = Mathf_Clamp_mEB9AEA827D27D20FCC787F7375156AF46BB12BBF_inline(((float)il2cpp_codegen_add((1.0f), L_30)), (0.0f), (2.0f), NULL);
			Vector2__ctor_m9525B79969AFFE3254B303A40997A56DEEB6F548_inline((&V_6), L_28, L_31, NULL);
			HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* L_32 = __this->___hapticsSdkPlaySample;
			int32_t L_33 = ___1_hand;
			Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_34 = V_6;
			NullCheck(L_32);
			HapticsSdkPlaySample_ModulateAmplitudeAndFrequencyOfFirstClip_m0B2D91A817207E188FA2154BCC4595689603ED51(L_32, L_33, L_34, NULL);
			goto IL_0146_1;
		}

IL_00ef_1:
		{
			int32_t L_35 = ___0_controller;
			V_7 = (bool)((((int32_t)L_35) == ((int32_t)2))? 1 : 0);
			bool L_36 = V_7;
			if (!L_36)
			{
				goto IL_0146_1;
			}
		}
		{
			il2cpp_codegen_runtime_class_init_inline(OVRInput_t759D9CC8F14D8539AD78A0375A2306C39D77100A_il2cpp_TypeInfo_var);
			Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_37;
			L_37 = OVRInput_Get_mB457003E7F3A6A8901B7A1D6CB6A167A5829E304(2, ((int32_t)-2147483648LL), NULL);
			float L_38 = L_37.___x;
			Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_39;
			L_39 = OVRInput_Get_mB457003E7F3A6A8901B7A1D6CB6A167A5829E304(2, ((int32_t)-2147483648LL), NULL);
			float L_40 = L_39.___y;
			float L_41;
			L_41 = Mathf_Clamp_mEB9AEA827D27D20FCC787F7375156AF46BB12BBF_inline(((float)il2cpp_codegen_add((1.0f), L_40)), (0.0f), (2.0f), NULL);
			Vector2__ctor_m9525B79969AFFE3254B303A40997A56DEEB6F548_inline((&V_8), L_38, L_41, NULL);
			HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* L_42 = __this->___hapticsSdkPlaySample;
			int32_t L_43 = ___1_hand;
			Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_44 = V_8;
			NullCheck(L_42);
			HapticsSdkPlaySample_ModulateAmplitudeAndFrequencyOfFirstClip_m0B2D91A817207E188FA2154BCC4595689603ED51(L_42, L_43, L_44, NULL);
		}

IL_0146_1:
		{
			goto IL_015c;
		}
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0149;
		}
		throw e;
	}

CATCH_0149:
	{
		Exception_t* L_45 = ((Exception_t*)IL2CPP_GET_ACTIVE_EXCEPTION(Exception_t*));;
		V_9 = L_45;
		Exception_t* L_46 = V_9;
		NullCheck(L_46);
		String_t* L_47;
		L_47 = VirtualFuncInvoker0< String_t* >::Invoke(5, L_46);
		il2cpp_codegen_runtime_class_init_inline(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var)));
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(L_47, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_015c;
	}

IL_015c:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkOVRControllerGuidance__ctor_m5D21717999DF225F3C76AC69B00348BFCE460C31 (HapticsSdkOVRControllerGuidance_t855408C22FF69F5FE72ABD5FE60021852704023C* __this, const RuntimeMethod* method) 
{
	{
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkPlaySample_Start_mB39D184E5418A6302A6A13A7E5EDCEE2DA67D0B5 (HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		HapticClip_tF8BCD1E73DCA942D4445DA6E85D0A3492D2799B6* L_0 = __this->___clip1;
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_1 = (HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349*)il2cpp_codegen_object_new(HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349_il2cpp_TypeInfo_var);
		HapticClipPlayer__ctor_m693F0978BAEA05D8F38243C88511E1F38AE068DA(L_1, L_0, NULL);
		__this->___leftClipPlayer1 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___leftClipPlayer1), (void*)L_1);
		HapticClip_tF8BCD1E73DCA942D4445DA6E85D0A3492D2799B6* L_2 = __this->___clip2;
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_3 = (HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349*)il2cpp_codegen_object_new(HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349_il2cpp_TypeInfo_var);
		HapticClipPlayer__ctor_m693F0978BAEA05D8F38243C88511E1F38AE068DA(L_3, L_2, NULL);
		__this->___leftClipPlayer2 = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___leftClipPlayer2), (void*)L_3);
		HapticClip_tF8BCD1E73DCA942D4445DA6E85D0A3492D2799B6* L_4 = __this->___clip1;
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_5 = (HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349*)il2cpp_codegen_object_new(HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349_il2cpp_TypeInfo_var);
		HapticClipPlayer__ctor_m693F0978BAEA05D8F38243C88511E1F38AE068DA(L_5, L_4, NULL);
		__this->___rightClipPlayer1 = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___rightClipPlayer1), (void*)L_5);
		HapticClip_tF8BCD1E73DCA942D4445DA6E85D0A3492D2799B6* L_6 = __this->___clip2;
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_7 = (HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349*)il2cpp_codegen_object_new(HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349_il2cpp_TypeInfo_var);
		HapticClipPlayer__ctor_m693F0978BAEA05D8F38243C88511E1F38AE068DA(L_7, L_6, NULL);
		__this->___rightClipPlayer2 = L_7;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___rightClipPlayer2), (void*)L_7);
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_8 = __this->___leftClipPlayer2;
		NullCheck(L_8);
		HapticClipPlayer_set_priority_m5F11CFACD6FF93D5B7628C090613E4143C9BA434(L_8, 1, NULL);
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_9 = __this->___rightClipPlayer2;
		NullCheck(L_9);
		HapticClipPlayer_set_priority_m5F11CFACD6FF93D5B7628C090613E4143C9BA434(L_9, 1, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkPlaySample_PlayFirstClip_mED4C03B8733C3C3B4B5794D69FE39DF35F488DD7 (HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* __this, int32_t ___0_hand, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral372DD8EE3DD1BF0CC9F51C568C39B9466C6D48C8);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE3CAC917A9440E06AF4AB8FE6B319C474FA111EF);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralFE85B285A9E71521E4E2B92C2F355FAC2744CACC);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		int32_t L_0 = ___0_hand;
		V_1 = L_0;
		int32_t L_1 = V_1;
		V_0 = L_1;
		int32_t L_2 = V_0;
		if (!L_2)
		{
			goto IL_001f;
		}
	}
	{
		goto IL_000a;
	}

IL_000a:
	{
		int32_t L_3 = V_0;
		if ((((int32_t)L_3) == ((int32_t)1)))
		{
			goto IL_0010;
		}
	}
	{
		goto IL_002e;
	}

IL_0010:
	{
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_4 = __this->___rightClipPlayer1;
		NullCheck(L_4);
		HapticClipPlayer_Play_mA82A3F90DF9A99942EA2BC41874BAF638E90B644(L_4, 1, NULL);
		goto IL_004d;
	}

IL_001f:
	{
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_5 = __this->___leftClipPlayer1;
		NullCheck(L_5);
		HapticClipPlayer_Play_mA82A3F90DF9A99942EA2BC41874BAF638E90B644(L_5, 0, NULL);
		goto IL_004d;
	}

IL_002e:
	{
		Il2CppFakeBox<int32_t> L_6(Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var, (&___0_hand));
		String_t* L_7;
		L_7 = Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741((Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2*)(&L_6), NULL);
		String_t* L_8;
		L_8 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteralE3CAC917A9440E06AF4AB8FE6B319C474FA111EF, L_7, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(L_8, NULL);
		goto IL_004d;
	}

IL_004d:
	{
		Il2CppFakeBox<int32_t> L_9(Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var, (&___0_hand));
		String_t* L_10;
		L_10 = Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741((Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2*)(&L_9), NULL);
		String_t* L_11;
		L_11 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(_stringLiteralFE85B285A9E71521E4E2B92C2F355FAC2744CACC, L_10, _stringLiteral372DD8EE3DD1BF0CC9F51C568C39B9466C6D48C8, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB(L_11, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkPlaySample_StopFirstClip_m5AD6B3AB085585458545BB2797EE2E38FEEDD44A (HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* __this, int32_t ___0_hand, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral133479D57A19A97DAD0A52860E28734914D6BA65);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE3CAC917A9440E06AF4AB8FE6B319C474FA111EF);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF3E84B722399601AD7E281754E917478AA9AD48D);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		int32_t L_0 = ___0_hand;
		V_1 = L_0;
		int32_t L_1 = V_1;
		V_0 = L_1;
		int32_t L_2 = V_0;
		if (!L_2)
		{
			goto IL_001e;
		}
	}
	{
		goto IL_000a;
	}

IL_000a:
	{
		int32_t L_3 = V_0;
		if ((((int32_t)L_3) == ((int32_t)1)))
		{
			goto IL_0010;
		}
	}
	{
		goto IL_002c;
	}

IL_0010:
	{
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_4 = __this->___rightClipPlayer1;
		NullCheck(L_4);
		HapticClipPlayer_Stop_m110F3977766D9399363EB9E06982023253AC9B19(L_4, NULL);
		goto IL_004b;
	}

IL_001e:
	{
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_5 = __this->___leftClipPlayer1;
		NullCheck(L_5);
		HapticClipPlayer_Stop_m110F3977766D9399363EB9E06982023253AC9B19(L_5, NULL);
		goto IL_004b;
	}

IL_002c:
	{
		Il2CppFakeBox<int32_t> L_6(Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var, (&___0_hand));
		String_t* L_7;
		L_7 = Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741((Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2*)(&L_6), NULL);
		String_t* L_8;
		L_8 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteralE3CAC917A9440E06AF4AB8FE6B319C474FA111EF, L_7, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(L_8, NULL);
		goto IL_004b;
	}

IL_004b:
	{
		Il2CppFakeBox<int32_t> L_9(Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var, (&___0_hand));
		String_t* L_10;
		L_10 = Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741((Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2*)(&L_9), NULL);
		String_t* L_11;
		L_11 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(_stringLiteral133479D57A19A97DAD0A52860E28734914D6BA65, L_10, _stringLiteralF3E84B722399601AD7E281754E917478AA9AD48D, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB(L_11, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkPlaySample_SetLoopingOfFirstClip_m79080372E878BA176F3DF8694AC70204DED920FB (HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* __this, int32_t ___0_hand, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral372DD8EE3DD1BF0CC9F51C568C39B9466C6D48C8);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralA65111E1AFCDCA0D36968D39E3597AE7538F6A27);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE3CAC917A9440E06AF4AB8FE6B319C474FA111EF);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		int32_t L_0 = ___0_hand;
		V_1 = L_0;
		int32_t L_1 = V_1;
		V_0 = L_1;
		int32_t L_2 = V_0;
		if (!L_2)
		{
			goto IL_0066;
		}
	}
	{
		goto IL_000a;
	}

IL_000a:
	{
		int32_t L_3 = V_0;
		if ((((int32_t)L_3) == ((int32_t)1)))
		{
			goto IL_0013;
		}
	}
	{
		goto IL_00b9;
	}

IL_0013:
	{
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_4 = __this->___rightClipPlayer1;
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_5 = __this->___rightClipPlayer1;
		NullCheck(L_5);
		bool L_6;
		L_6 = HapticClipPlayer_get_isLooping_m1A05073308EA34D9B8F96980993C33BD10A020E3(L_5, NULL);
		NullCheck(L_4);
		HapticClipPlayer_set_isLooping_mECB0B0F5E896B2C12E00EB4238E1BA821A6535C5(L_4, (bool)((((int32_t)L_6) == ((int32_t)0))? 1 : 0), NULL);
		Il2CppFakeBox<int32_t> L_7(Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var, (&___0_hand));
		String_t* L_8;
		L_8 = Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741((Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2*)(&L_7), NULL);
		String_t* L_9;
		L_9 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(_stringLiteralA65111E1AFCDCA0D36968D39E3597AE7538F6A27, L_8, _stringLiteral372DD8EE3DD1BF0CC9F51C568C39B9466C6D48C8, NULL);
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_10 = __this->___rightClipPlayer1;
		NullCheck(L_10);
		bool L_11;
		L_11 = HapticClipPlayer_get_isLooping_m1A05073308EA34D9B8F96980993C33BD10A020E3(L_10, NULL);
		bool L_12 = L_11;
		RuntimeObject* L_13 = Box(il2cpp_defaults.boolean_class, &L_12);
		String_t* L_14;
		L_14 = String_Format_mA8DBB4C2516B9723C5A41E6CB1E2FAF4BBE96DD8(L_9, L_13, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB(L_14, NULL);
		goto IL_00d8;
	}

IL_0066:
	{
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_15 = __this->___leftClipPlayer1;
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_16 = __this->___leftClipPlayer1;
		NullCheck(L_16);
		bool L_17;
		L_17 = HapticClipPlayer_get_isLooping_m1A05073308EA34D9B8F96980993C33BD10A020E3(L_16, NULL);
		NullCheck(L_15);
		HapticClipPlayer_set_isLooping_mECB0B0F5E896B2C12E00EB4238E1BA821A6535C5(L_15, (bool)((((int32_t)L_17) == ((int32_t)0))? 1 : 0), NULL);
		Il2CppFakeBox<int32_t> L_18(Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var, (&___0_hand));
		String_t* L_19;
		L_19 = Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741((Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2*)(&L_18), NULL);
		String_t* L_20;
		L_20 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(_stringLiteralA65111E1AFCDCA0D36968D39E3597AE7538F6A27, L_19, _stringLiteral372DD8EE3DD1BF0CC9F51C568C39B9466C6D48C8, NULL);
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_21 = __this->___leftClipPlayer1;
		NullCheck(L_21);
		bool L_22;
		L_22 = HapticClipPlayer_get_isLooping_m1A05073308EA34D9B8F96980993C33BD10A020E3(L_21, NULL);
		bool L_23 = L_22;
		RuntimeObject* L_24 = Box(il2cpp_defaults.boolean_class, &L_23);
		String_t* L_25;
		L_25 = String_Format_mA8DBB4C2516B9723C5A41E6CB1E2FAF4BBE96DD8(L_20, L_24, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB(L_25, NULL);
		goto IL_00d8;
	}

IL_00b9:
	{
		Il2CppFakeBox<int32_t> L_26(Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var, (&___0_hand));
		String_t* L_27;
		L_27 = Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741((Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2*)(&L_26), NULL);
		String_t* L_28;
		L_28 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteralE3CAC917A9440E06AF4AB8FE6B319C474FA111EF, L_27, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(L_28, NULL);
		goto IL_00d8;
	}

IL_00d8:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkPlaySample_ModulateAmplitudeAndFrequencyOfFirstClip_m0B2D91A817207E188FA2154BCC4595689603ED51 (HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* __this, int32_t ___0_hand, Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___1_input, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE3CAC917A9440E06AF4AB8FE6B319C474FA111EF);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		int32_t L_0 = ___0_hand;
		V_1 = L_0;
		int32_t L_1 = V_1;
		V_0 = L_1;
		int32_t L_2 = V_0;
		if (!L_2)
		{
			goto IL_0036;
		}
	}
	{
		goto IL_000a;
	}

IL_000a:
	{
		int32_t L_3 = V_0;
		if ((((int32_t)L_3) == ((int32_t)1)))
		{
			goto IL_0010;
		}
	}
	{
		goto IL_005c;
	}

IL_0010:
	{
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_4 = __this->___rightClipPlayer1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_5 = ___1_input;
		float L_6 = L_5.___y;
		NullCheck(L_4);
		HapticClipPlayer_set_amplitude_mBDD32378F8338E8611D8D57AF9690129BBEB8CBD(L_4, L_6, NULL);
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_7 = __this->___rightClipPlayer1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_8 = ___1_input;
		float L_9 = L_8.___x;
		NullCheck(L_7);
		HapticClipPlayer_set_frequencyShift_m1787B7594F4E870727FDB3EEBED1FA5C5DE7EDF0(L_7, L_9, NULL);
		goto IL_007b;
	}

IL_0036:
	{
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_10 = __this->___leftClipPlayer1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_11 = ___1_input;
		float L_12 = L_11.___y;
		NullCheck(L_10);
		HapticClipPlayer_set_amplitude_mBDD32378F8338E8611D8D57AF9690129BBEB8CBD(L_10, L_12, NULL);
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_13 = __this->___leftClipPlayer1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_14 = ___1_input;
		float L_15 = L_14.___x;
		NullCheck(L_13);
		HapticClipPlayer_set_frequencyShift_m1787B7594F4E870727FDB3EEBED1FA5C5DE7EDF0(L_13, L_15, NULL);
		goto IL_007b;
	}

IL_005c:
	{
		Il2CppFakeBox<int32_t> L_16(Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var, (&___0_hand));
		String_t* L_17;
		L_17 = Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741((Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2*)(&L_16), NULL);
		String_t* L_18;
		L_18 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteralE3CAC917A9440E06AF4AB8FE6B319C474FA111EF, L_17, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(L_18, NULL);
		goto IL_007b;
	}

IL_007b:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkPlaySample_PlaySecondClip_m5E3830B68B705FDDCF75EB75161AD24084FEEF12 (HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* __this, int32_t ___0_hand, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral372DD8EE3DD1BF0CC9F51C568C39B9466C6D48C8);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral69969C56A91534E22881689E82CE3DA725CF0CF1);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE3CAC917A9440E06AF4AB8FE6B319C474FA111EF);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		int32_t L_0 = ___0_hand;
		V_1 = L_0;
		int32_t L_1 = V_1;
		V_0 = L_1;
		int32_t L_2 = V_0;
		if (!L_2)
		{
			goto IL_001f;
		}
	}
	{
		goto IL_000a;
	}

IL_000a:
	{
		int32_t L_3 = V_0;
		if ((((int32_t)L_3) == ((int32_t)1)))
		{
			goto IL_0010;
		}
	}
	{
		goto IL_002e;
	}

IL_0010:
	{
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_4 = __this->___rightClipPlayer2;
		NullCheck(L_4);
		HapticClipPlayer_Play_mA82A3F90DF9A99942EA2BC41874BAF638E90B644(L_4, 1, NULL);
		goto IL_004d;
	}

IL_001f:
	{
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_5 = __this->___leftClipPlayer2;
		NullCheck(L_5);
		HapticClipPlayer_Play_mA82A3F90DF9A99942EA2BC41874BAF638E90B644(L_5, 0, NULL);
		goto IL_004d;
	}

IL_002e:
	{
		Il2CppFakeBox<int32_t> L_6(Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var, (&___0_hand));
		String_t* L_7;
		L_7 = Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741((Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2*)(&L_6), NULL);
		String_t* L_8;
		L_8 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteralE3CAC917A9440E06AF4AB8FE6B319C474FA111EF, L_7, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(L_8, NULL);
		goto IL_004d;
	}

IL_004d:
	{
		Il2CppFakeBox<int32_t> L_9(Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var, (&___0_hand));
		String_t* L_10;
		L_10 = Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741((Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2*)(&L_9), NULL);
		String_t* L_11;
		L_11 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(_stringLiteral69969C56A91534E22881689E82CE3DA725CF0CF1, L_10, _stringLiteral372DD8EE3DD1BF0CC9F51C568C39B9466C6D48C8, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB(L_11, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkPlaySample_StopSecondClip_mADBEE7F665F7FF3617EAC4B7B3EAA24FC337A500 (HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* __this, int32_t ___0_hand, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral46D551A851A702EF3CB05D5D664916D4996E850C);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE3CAC917A9440E06AF4AB8FE6B319C474FA111EF);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF3E84B722399601AD7E281754E917478AA9AD48D);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		int32_t L_0 = ___0_hand;
		V_1 = L_0;
		int32_t L_1 = V_1;
		V_0 = L_1;
		int32_t L_2 = V_0;
		if (!L_2)
		{
			goto IL_001e;
		}
	}
	{
		goto IL_000a;
	}

IL_000a:
	{
		int32_t L_3 = V_0;
		if ((((int32_t)L_3) == ((int32_t)1)))
		{
			goto IL_0010;
		}
	}
	{
		goto IL_002c;
	}

IL_0010:
	{
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_4 = __this->___rightClipPlayer2;
		NullCheck(L_4);
		HapticClipPlayer_Stop_m110F3977766D9399363EB9E06982023253AC9B19(L_4, NULL);
		goto IL_004b;
	}

IL_001e:
	{
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_5 = __this->___leftClipPlayer2;
		NullCheck(L_5);
		HapticClipPlayer_Stop_m110F3977766D9399363EB9E06982023253AC9B19(L_5, NULL);
		goto IL_004b;
	}

IL_002c:
	{
		Il2CppFakeBox<int32_t> L_6(Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var, (&___0_hand));
		String_t* L_7;
		L_7 = Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741((Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2*)(&L_6), NULL);
		String_t* L_8;
		L_8 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteralE3CAC917A9440E06AF4AB8FE6B319C474FA111EF, L_7, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(L_8, NULL);
		goto IL_004b;
	}

IL_004b:
	{
		Il2CppFakeBox<int32_t> L_9(Controller_t1D1A89BC4E15A9E62E085897508A866454C7185D_il2cpp_TypeInfo_var, (&___0_hand));
		String_t* L_10;
		L_10 = Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741((Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2*)(&L_9), NULL);
		String_t* L_11;
		L_11 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(_stringLiteral46D551A851A702EF3CB05D5D664916D4996E850C, L_10, _stringLiteralF3E84B722399601AD7E281754E917478AA9AD48D, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB(L_11, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkPlaySample_OnDestroy_m518CD6DB33F42B7A7FBE89511162D0F5E794ABCC (HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* __this, const RuntimeMethod* method) 
{
	HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* G_B2_0 = NULL;
	HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* G_B1_0 = NULL;
	HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* G_B5_0 = NULL;
	HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* G_B4_0 = NULL;
	HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* G_B8_0 = NULL;
	HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* G_B7_0 = NULL;
	HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* G_B11_0 = NULL;
	HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* G_B10_0 = NULL;
	{
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_0 = __this->___leftClipPlayer1;
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000d;
		}
		G_B1_0 = L_1;
	}
	{
		goto IL_0013;
	}

IL_000d:
	{
		NullCheck(G_B2_0);
		HapticClipPlayer_Dispose_m64F1567B14DE1504A97535361A6EA9838266527E(G_B2_0, NULL);
	}

IL_0013:
	{
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_2 = __this->___leftClipPlayer2;
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_3 = L_2;
		if (L_3)
		{
			G_B5_0 = L_3;
			goto IL_001f;
		}
		G_B4_0 = L_3;
	}
	{
		goto IL_0025;
	}

IL_001f:
	{
		NullCheck(G_B5_0);
		HapticClipPlayer_Dispose_m64F1567B14DE1504A97535361A6EA9838266527E(G_B5_0, NULL);
	}

IL_0025:
	{
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_4 = __this->___rightClipPlayer1;
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_5 = L_4;
		if (L_5)
		{
			G_B8_0 = L_5;
			goto IL_0031;
		}
		G_B7_0 = L_5;
	}
	{
		goto IL_0037;
	}

IL_0031:
	{
		NullCheck(G_B8_0);
		HapticClipPlayer_Dispose_m64F1567B14DE1504A97535361A6EA9838266527E(G_B8_0, NULL);
	}

IL_0037:
	{
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_6 = __this->___rightClipPlayer2;
		HapticClipPlayer_t52676339D3F2A8BEC736EAE3EB17AEBF18BD3349* L_7 = L_6;
		if (L_7)
		{
			G_B11_0 = L_7;
			goto IL_0043;
		}
		G_B10_0 = L_7;
	}
	{
		goto IL_0049;
	}

IL_0043:
	{
		NullCheck(G_B11_0);
		HapticClipPlayer_Dispose_m64F1567B14DE1504A97535361A6EA9838266527E(G_B11_0, NULL);
	}

IL_0049:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkPlaySample_OnApplicationQuit_m06DE66D30FEE7B8A5EE9FDE4047AD0E623CB0CF9 (HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* __this, const RuntimeMethod* method) 
{
	{
		Haptics_t93216D86CF0A99726A61DDED24C12CAFBACE5F7B* L_0;
		L_0 = Haptics_get_Instance_m949CEF3AA5651088AC4B3991580137E0EAA1D819(NULL);
		NullCheck(L_0);
		Haptics_Dispose_mB478AF6745C494BF898DCFBA07E593635F9FBAAB(L_0, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HapticsSdkPlaySample__ctor_m0EEDEA1775F2229677B214941F9A3714F64F562E (HapticsSdkPlaySample_t65F1C5249C8F4059BF800788FEF0C7EE44725046* __this, const RuntimeMethod* method) 
{
	{
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B UnitySourceGeneratedAssemblyMonoScriptTypes_v1_Get_m2B68F10BC46BAD5272688150A8A255C0A43CAAD2 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CPrivateImplementationDetailsU3E_tE8C75C91D58BEC9F762459030BB60F85C1184FEA____BB6DDED0FD805DD922372D0F899FCB2DDE7A59BCDBFEE8018B1C89B6C6216CA1_FieldInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CPrivateImplementationDetailsU3E_tE8C75C91D58BEC9F762459030BB60F85C1184FEA____FD9ACF6733B5D027865E9899C3D90D58A3AADA0076058460270A1BAB8B35F378_FieldInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B V_0;
	memset((&V_0), 0, sizeof(V_0));
	MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		il2cpp_codegen_initobj((&V_0), sizeof(MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B));
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)SZArrayNew(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var, (uint32_t)((int32_t)351));
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_1 = L_0;
		RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 L_2 = { reinterpret_cast<intptr_t> (U3CPrivateImplementationDetailsU3E_tE8C75C91D58BEC9F762459030BB60F85C1184FEA____FD9ACF6733B5D027865E9899C3D90D58A3AADA0076058460270A1BAB8B35F378_FieldInfo_var) };
		RuntimeHelpers_InitializeArray_m751372AA3F24FBF6DA9B9D687CBFA2DE436CAB9B((RuntimeArray*)L_1, L_2, NULL);
		(&V_0)->___FilePathsData = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&(&V_0)->___FilePathsData), (void*)L_1);
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_3 = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)SZArrayNew(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var, (uint32_t)((int32_t)87));
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_4 = L_3;
		RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 L_5 = { reinterpret_cast<intptr_t> (U3CPrivateImplementationDetailsU3E_tE8C75C91D58BEC9F762459030BB60F85C1184FEA____BB6DDED0FD805DD922372D0F899FCB2DDE7A59BCDBFEE8018B1C89B6C6216CA1_FieldInfo_var) };
		RuntimeHelpers_InitializeArray_m751372AA3F24FBF6DA9B9D687CBFA2DE436CAB9B((RuntimeArray*)L_4, L_5, NULL);
		(&V_0)->___TypesData = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&(&V_0)->___TypesData), (void*)L_4);
		(&V_0)->___TotalFiles = 3;
		(&V_0)->___TotalTypes = 3;
		(&V_0)->___IsEditorOnly = (bool)0;
		MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B L_6 = V_0;
		V_1 = L_6;
		goto IL_005a;
	}

IL_005a:
	{
		MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B L_7 = V_1;
		return L_7;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnitySourceGeneratedAssemblyMonoScriptTypes_v1__ctor_m9AFC41CB0CD3FEFCCD4F696ADAB32E8376983A3E (UnitySourceGeneratedAssemblyMonoScriptTypes_v1_tB77C605CD8AC458B3B1B8099F527E54E3CA8523E* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C void MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B_marshal_pinvoke(const MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B& unmarshaled, MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B_marshaled_pinvoke& marshaled)
{
	marshaled.___FilePathsData = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___FilePathsData);
	marshaled.___TypesData = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___TypesData);
	marshaled.___TotalTypes = unmarshaled.___TotalTypes;
	marshaled.___TotalFiles = unmarshaled.___TotalFiles;
	marshaled.___IsEditorOnly = static_cast<int32_t>(unmarshaled.___IsEditorOnly);
}
IL2CPP_EXTERN_C void MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B_marshal_pinvoke_back(const MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B_marshaled_pinvoke& marshaled, MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B& unmarshaled)
{
	unmarshaled.___FilePathsData = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___FilePathsData);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___FilePathsData), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___FilePathsData));
	unmarshaled.___TypesData = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___TypesData);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___TypesData), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___TypesData));
	int32_t unmarshaledTotalTypes_temp_2 = 0;
	unmarshaledTotalTypes_temp_2 = marshaled.___TotalTypes;
	unmarshaled.___TotalTypes = unmarshaledTotalTypes_temp_2;
	int32_t unmarshaledTotalFiles_temp_3 = 0;
	unmarshaledTotalFiles_temp_3 = marshaled.___TotalFiles;
	unmarshaled.___TotalFiles = unmarshaledTotalFiles_temp_3;
	bool unmarshaledIsEditorOnly_temp_4 = false;
	unmarshaledIsEditorOnly_temp_4 = static_cast<bool>(marshaled.___IsEditorOnly);
	unmarshaled.___IsEditorOnly = unmarshaledIsEditorOnly_temp_4;
}
IL2CPP_EXTERN_C void MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B_marshal_pinvoke_cleanup(MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B_marshaled_pinvoke& marshaled)
{
	il2cpp_codegen_com_destroy_safe_array(marshaled.___FilePathsData);
	marshaled.___FilePathsData = NULL;
	il2cpp_codegen_com_destroy_safe_array(marshaled.___TypesData);
	marshaled.___TypesData = NULL;
}
IL2CPP_EXTERN_C void MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B_marshal_com(const MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B& unmarshaled, MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B_marshaled_com& marshaled)
{
	marshaled.___FilePathsData = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___FilePathsData);
	marshaled.___TypesData = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___TypesData);
	marshaled.___TotalTypes = unmarshaled.___TotalTypes;
	marshaled.___TotalFiles = unmarshaled.___TotalFiles;
	marshaled.___IsEditorOnly = static_cast<int32_t>(unmarshaled.___IsEditorOnly);
}
IL2CPP_EXTERN_C void MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B_marshal_com_back(const MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B_marshaled_com& marshaled, MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B& unmarshaled)
{
	unmarshaled.___FilePathsData = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___FilePathsData);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___FilePathsData), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___FilePathsData));
	unmarshaled.___TypesData = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___TypesData);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___TypesData), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___TypesData));
	int32_t unmarshaledTotalTypes_temp_2 = 0;
	unmarshaledTotalTypes_temp_2 = marshaled.___TotalTypes;
	unmarshaled.___TotalTypes = unmarshaledTotalTypes_temp_2;
	int32_t unmarshaledTotalFiles_temp_3 = 0;
	unmarshaledTotalFiles_temp_3 = marshaled.___TotalFiles;
	unmarshaled.___TotalFiles = unmarshaledTotalFiles_temp_3;
	bool unmarshaledIsEditorOnly_temp_4 = false;
	unmarshaledIsEditorOnly_temp_4 = static_cast<bool>(marshaled.___IsEditorOnly);
	unmarshaled.___IsEditorOnly = unmarshaledIsEditorOnly_temp_4;
}
IL2CPP_EXTERN_C void MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B_marshal_com_cleanup(MonoScriptData_tF38E1661D23F66F3827399A54E8B36BAFF0BD60B_marshaled_com& marshaled)
{
	il2cpp_codegen_com_destroy_safe_array(marshaled.___FilePathsData);
	marshaled.___FilePathsData = NULL;
	il2cpp_codegen_com_destroy_safe_array(marshaled.___TypesData);
	marshaled.___TypesData = NULL;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void HapticsSdkGuidance_set_CurrentPopUpIndex_m7C8E4956FB861834E3CD2368DC6EB4D6AFF0D3B5_inline (int32_t ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		int32_t L_0 = ___0_value;
		((HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D_StaticFields*)il2cpp_codegen_static_fields_for(HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D_il2cpp_TypeInfo_var))->___U3CCurrentPopUpIndexU3Ek__BackingField = L_0;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t HapticsSdkGuidance_get_CurrentPopUpIndex_mF6127869105DFDC1A16952E34F0AD1148DF4D02A_inline (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		int32_t L_0 = ((HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D_StaticFields*)il2cpp_codegen_static_fields_for(HapticsSdkGuidance_t696D957456AD465DB537E530DFE38532A7FE8A1D_il2cpp_TypeInfo_var))->___U3CCurrentPopUpIndexU3Ek__BackingField;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Clamp_mEB9AEA827D27D20FCC787F7375156AF46BB12BBF_inline (float ___0_value, float ___1_min, float ___2_max, const RuntimeMethod* method) 
{
	bool V_0 = false;
	bool V_1 = false;
	float V_2 = 0.0f;
	{
		float L_0 = ___0_value;
		float L_1 = ___1_min;
		V_0 = (bool)((((float)L_0) < ((float)L_1))? 1 : 0);
		bool L_2 = V_0;
		if (!L_2)
		{
			goto IL_000e;
		}
	}
	{
		float L_3 = ___1_min;
		___0_value = L_3;
		goto IL_0019;
	}

IL_000e:
	{
		float L_4 = ___0_value;
		float L_5 = ___2_max;
		V_1 = (bool)((((float)L_4) > ((float)L_5))? 1 : 0);
		bool L_6 = V_1;
		if (!L_6)
		{
			goto IL_0019;
		}
	}
	{
		float L_7 = ___2_max;
		___0_value = L_7;
	}

IL_0019:
	{
		float L_8 = ___0_value;
		V_2 = L_8;
		goto IL_001d;
	}

IL_001d:
	{
		float L_9 = V_2;
		return L_9;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Vector2__ctor_m9525B79969AFFE3254B303A40997A56DEEB6F548_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* __this, float ___0_x, float ___1_y, const RuntimeMethod* method) 
{
	{
		float L_0 = ___0_x;
		__this->___x = L_0;
		float L_1 = ___1_y;
		__this->___y = L_1;
		return;
	}
}

Public Class cls_MediaEncoderParameters

    ' Set up each device name
    Public Const DEVICE_NAME_APPLETV As String = "APPLETV"
    Public Const DEVICE_NAME_BLACKBERRY8100 As String = "BLACKBERRY8100"
    Public Const DEVICE_NAME_BLACKBERRY8200 As String = "BLACKBERRY8200"
    Public Const DEVICE_NAME_BLACKBERRY8300 As String = "BLACKBERRY8300"
    Public Const DEVICE_NAME_BLACKBERRY8700 As String = "BLACKBERRY8700"
    Public Const DEVICE_NAME_BLACKBERRY8800 As String = "BLACKBERRY8800"
    Public Const DEVICE_NAME_BLACKBERRY8900 As String = "BLACKBERRY8900"
    Public Const DEVICE_NAME_BLACKBERRY9000 As String = "BLACKBERRY9000"
    Public Const DEVICE_NAME_BLACKBERRY9500 As String = "BLACKBERRY9500"
    Public Const DEVICE_NAME_IPHONE As String = "IPHONE"
    Public Const DEVICE_NAME_IPOD5G As String = "IPOD5G"
    Public Const DEVICE_NAME_IPODCLASSIC As String = "IPODCLASSIC"
    Public Const DEVICE_NAME_IPODNANO As String = "IPODNANO"
    Public Const DEVICE_NAME_IPODTOUCH As String = "IPODTOUCH"
    Public Const DEVICE_NAME_NOKIAE71 As String = "NOKIAE71"
    Public Const DEVICE_NAME_PLAYSTATION3 As String = "PLAYSTATION3"
    Public Const DEVICE_NAME_PSP As String = "PSP"
    Public Const DEVICE_NAME_TMOBILEG1 As String = "TMOBILEG1"
    Public Const DEVICE_NAME_WESTERNDIGITALDV As String = "WESTERNDIGITALTV"
    Public Const DEVICE_NAME_XBOX360 As String = "XBOX360"
    Public Const DEVICE_NAME_YOUTUBEHD As String = "YOUTUBEHD"
    Public Const DEVICE_NAME_ZUNE As String = "ZUNE"
    Public Const DEVICE_NAME_ZUNEHD As String = "ZUNEHD"

    Public Const OUTPUT_FILEEXTENSION_STANDARD As String = "MP4"
    Public Const OUTPUT_FILEEXTENSION_AC3PASSTHROUGH As String = "M4V"

    ' Input File Properties
    Private _str_Input_Video_Codec As String = Nothing
    Private _int_Input_Video_Duration As Integer = 0
    Private _int_Input_Video_BitRate As Integer = 0
    Private _int_Input_Video_Height As Integer = 0
    Private _int_Input_Video_Width As Integer = 0
    Private _dbl_Input_Video_FPS As Double = 0
    Private _dbl_Input_Video_AspectRatio As Double = 0
    Private _int_Input_Audio_Stream As Integer = 0
    Private _str_Input_Audio_Codec As String = Nothing
    Private _int_Input_Audio_BitRate As Integer = 0
    Private _int_Input_Audio_Channels As Integer = 0
    Private _int_Input_Audio_SampleRate As Integer = 0

    Private _str_Output_Device As String = Nothing

    Private _bln_Output_Video_Codec_H264Supported As Boolean = False
    Private _bln_Output_Video_Codec_H264Enabled As Boolean = False
    Private _bln_Output_Video_Resolution_OutputForTV As Boolean = False
    Private _bln_Output_Audio_Codec_AC3PassThroughEnabled As Boolean = False

    ' Filtered Output File Properties
    Private _str_Output_Video_FileExtension As String = Nothing
    Private _str_Output_Video_Codec As String = Nothing
    Private _int_Output_Video_Duration As Integer = Nothing
    Private _int_Output_Video_BitRate As Integer = 0
    Private _int_Output_Video_Height As Integer = 0
    Private _int_Output_Video_Width As Integer = 0
    Private _int_Output_Video_Padding_Top As Integer
    Private _int_Output_Video_Padding_Bottom As Integer
    Private _int_Output_Video_H264_Profile_Level As Integer = 0
    Private _bln_Output_Video_H264_Profile_LowComplexity As Boolean = False
    Private _str_Output_Video_H264_Profile_UUID As String = Nothing
    Private _dbl_Output_Video_FPS As Double = 0
    Private _str_Output_Audio_Codecs As String() = Nothing
    Private _int_Output_Audio_Stream As Integer = 0
    Private _int_Output_Audio_BitRate As Integer = 0
    Private _int_Output_Audio_Channels As Integer = 0
    Private _int_Output_Audio_SampleRate As Integer = 0

    Public Structure EncodingSpecifications
        Public VIDEO_FILE_EXTENSION As String
        Public VIDEO_H264_FPS_FORCED As Double
        Public VIDEO_H264_FPS_24_RESOLUTION_WIDTH_MAX As Integer
        Public VIDEO_H264_FPS_24_RESOLUTION_HEIGHT_MAX As Integer
        Public VIDEO_H264_FPS_25_RESOLUTION_WIDTH_MAX As Integer
        Public VIDEO_H264_FPS_25_RESOLUTION_HEIGHT_MAX As Integer
        Public VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX As Integer
        Public VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX As Integer
        Public VIDEO_H264_BITRATE_MAX As Integer
        Public VIDEO_H264_PROFILE_LEVEL As Integer
        Public VIDEO_H264_PROFILE_LOWCOMPLEXITY As Boolean
        Public VIDEO_H264_PROFILE_UUID As String
        Public VIDEO_H264_AR_PADDING As Boolean
        Public VIDEO_MPEG4_FPS_FORCED As Double
        Public VIDEO_MPEG4_FPS_24_RESOLUTION_WIDTH_MAX As Integer
        Public VIDEO_MPEG4_FPS_24_RESOLUTION_HEIGHT_MAX As Integer
        Public VIDEO_MPEG4_FPS_25_RESOLUTION_WIDTH_MAX As Integer
        Public VIDEO_MPEG4_FPS_25_RESOLUTION_HEIGHT_MAX As Integer
        Public VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX As Integer
        Public VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX As Integer
        Public VIDEO_MPEG4_BITRATE_MAX As Integer
        Public AUDIO_AAC_BITRATE_MAX As Integer
        Public AUDIO_AAC_SAMPLERATE_MAX As Integer
        Public AUDIO_AAC_CHANNELS_MAX As Integer
        Public AUDIO_MP3_BITRATE_MAX As Integer
        Public AUDIO_MP3_SAMPLERATE_MAX As Integer
        Public AUDIO_MP3_CHANNELS_MAX As Integer
        Public AUDIO_AC3_OUTPUT_ONLY As Boolean
        Public AUDIO_AC3_BITRATE_MAX As Integer
        Public AUDIO_AC3_SAMPLERATE_MAX As Integer
        Public AUDIO_AC3_CHANNELS_MAX As Integer
    End Structure

    Public Structure EncodingVideoOutput
        Public VIDEO_RESOLUTION_WIDTH_MAX As Integer
        Public VIDEO_RESOLUTION_HEIGHT_MAX As Integer
        Public VIDEO_BITRATE_MAX As Integer
    End Structure


    Private SPEC_APPLETV_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_APPLETV_AUDIO_CODECS_SUPPORTED As String() = {"AAC", "AC3"}
    Private SPEC_APPLETV_VIDEO_TVOUTPUT_SUPPORTED As Boolean = False

    Private SPEC_BLACKBERRY8100_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4"}
    Private SPEC_BLACKBERRY8100_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_BLACKBERRY8100_VIDEO_TVOUTPUT_SUPPORTED As Boolean = False

    Private SPEC_BLACKBERRY8200_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4"}
    Private SPEC_BLACKBERRY8200_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_BLACKBERRY8200_VIDEO_TVOUTPUT_SUPPORTED As Boolean = False

    Private SPEC_BLACKBERRY8300_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4"}
    Private SPEC_BLACKBERRY8300_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_BLACKBERRY8300_VIDEO_TVOUTPUT_SUPPORTED As Boolean = False

    Private SPEC_BLACKBERRY8700_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4"}
    Private SPEC_BLACKBERRY8700_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_BLACKBERRY8700_VIDEO_TVOUTPUT_SUPPORTED As Boolean = False

    Private SPEC_BLACKBERRY8800_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4"}
    Private SPEC_BLACKBERRY8800_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_BLACKBERRY8800_VIDEO_TVOUTPUT_SUPPORTED As Boolean = False

    Private SPEC_BLACKBERRY8900_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4"}
    Private SPEC_BLACKBERRY8900_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_BLACKBERRY8900_VIDEO_TVOUTPUT_SUPPORTED As Boolean = False

    Private SPEC_BLACKBERRY9000_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_BLACKBERRY9000_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_BLACKBERRY9000_VIDEO_TVOUTPUT_SUPPORTED As Boolean = False

    Private SPEC_BLACKBERRY9500_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_BLACKBERRY9500_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_BLACKBERRY9500_VIDEO_TVOUTPUT_SUPPORTED As Boolean = False

    Private SPEC_IPHONE_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_IPHONE_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_IPHONE_VIDEO_TVOUTPUT_SUPPORTED As Boolean = True

    Private SPEC_IPOD5G_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_IPOD5G_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_IPOD5G_VIDEO_TVOUTPUT_SUPPORTED As Boolean = True

    Private SPEC_IPODCLASSIC_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_IPODCLASSIC_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_IPODCLASSIC_VIDEO_TVOUTPUT_SUPPORTED As Boolean = True

    Private SPEC_IPODNANO_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_IPODNANO_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_IPODNANO_VIDEO_TVOUTPUT_SUPPORTED As Boolean = True

    Private SPEC_IPODTOUCH_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_IPODTOUCH_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_IPODTOUCH_VIDEO_TVOUTPUT_SUPPORTED As Boolean = True

    Private SPEC_NOKIAE71_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_NOKIAE71_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_NOKIAE71_VIDEO_TVOUTPUT_SUPPORTED As Boolean = False

    Private SPEC_PLAYSTATION3_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_PLAYSTATION3_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_PLAYSTATION3_VIDEO_TVOUTPUT_SUPPORTED As Boolean = False

    Private SPEC_PSP_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_PSP_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_PSP_VIDEO_TVOUTPUT_SUPPORTED As Boolean = False

    Private SPEC_TMOBILEG1_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_TMOBILEG1_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_TMOBILEG1_VIDEO_TVOUTPUT_SUPPORTED As Boolean = False

    Private SPEC_WESTERNDIGITALTV_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_WESTERNDIGITALTV_AUDIO_CODECS_SUPPORTED As String() = {"AAC", "AC3"}
    Private SPEC_WESTERNDIGITALTV_VIDEO_TVOUTPUT_SUPPORTED As Boolean = False

    Private SPEC_XBOX360_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_XBOX360_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_XBOX360_VIDEO_TVOUTPUT_SUPPORTED As Boolean = False

    Private SPEC_YOUTUBEHD_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_YOUTUBEHD_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_YOUTUBEHD_VIDEO_TVOUTPUT_SUPPORTED As Boolean = False

    Private SPEC_ZUNE_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_ZUNE_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_ZUNE_VIDEO_TVOUTPUT_SUPPORTED As Boolean = True

    Private SPEC_ZUNEHD_VIDEO_CODECS_SUPPORTED As String() = {"MPEG4", "H264"}
    Private SPEC_ZUNEHD_AUDIO_CODECS_SUPPORTED As String() = {"AAC"}
    Private SPEC_ZUNEHD_VIDEO_TVOUTPUT_SUPPORTED As Boolean = True

#Region "Properties"

    ' Sets / Gets the Output Device
    Public Property Output_Device() As String
        Get
            Return _str_Output_Device
        End Get
        Set(ByVal value As String)
            _str_Output_Device = value
        End Set
    End Property

    ' Returns whether this device supports H264 Encoding
    Public ReadOnly Property Output_Video_Codec_H264Supported() As Boolean
        Get
            Dim _str_SupportedCodecs As String() = Nothing

            ' Enumrate the type of device
            Select Case _str_Output_Device
                Case DEVICE_NAME_APPLETV
                    _str_SupportedCodecs = SPEC_APPLETV_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_BLACKBERRY8100
                    _str_SupportedCodecs = SPEC_BLACKBERRY8100_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_BLACKBERRY8200
                    _str_SupportedCodecs = SPEC_BLACKBERRY8200_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_BLACKBERRY8300
                    _str_SupportedCodecs = SPEC_BLACKBERRY8300_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_BLACKBERRY8700
                    _str_SupportedCodecs = SPEC_BLACKBERRY8700_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_BLACKBERRY8800
                    _str_SupportedCodecs = SPEC_BLACKBERRY8800_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_BLACKBERRY8900
                    _str_SupportedCodecs = SPEC_BLACKBERRY8900_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_BLACKBERRY9000
                    _str_SupportedCodecs = SPEC_BLACKBERRY9000_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_BLACKBERRY9500
                    _str_SupportedCodecs = SPEC_BLACKBERRY9500_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_IPHONE
                    _str_SupportedCodecs = SPEC_IPHONE_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_IPOD5G
                    _str_SupportedCodecs = SPEC_IPOD5G_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_IPODCLASSIC
                    _str_SupportedCodecs = SPEC_IPODCLASSIC_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_IPODNANO
                    _str_SupportedCodecs = SPEC_IPODNANO_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_IPODTOUCH
                    _str_SupportedCodecs = SPEC_IPODTOUCH_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_NOKIAE71
                    _str_SupportedCodecs = SPEC_NOKIAE71_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_PLAYSTATION3
                    _str_SupportedCodecs = SPEC_PLAYSTATION3_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_PSP
                    _str_SupportedCodecs = SPEC_PSP_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_TMOBILEG1
                    _str_SupportedCodecs = SPEC_TMOBILEG1_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_WESTERNDIGITALDV
                    _str_SupportedCodecs = SPEC_WESTERNDIGITALTV_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_XBOX360
                    _str_SupportedCodecs = SPEC_XBOX360_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_YOUTUBEHD
                    _str_SupportedCodecs = SPEC_YOUTUBEHD_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_ZUNE
                    _str_SupportedCodecs = SPEC_ZUNE_VIDEO_CODECS_SUPPORTED
                Case DEVICE_NAME_ZUNEHD
                    _str_SupportedCodecs = SPEC_ZUNEHD_VIDEO_CODECS_SUPPORTED
                Case Else
                    ' Return False if no match found
                    Return False
            End Select

            ' Check if it supports the H264 codec
            For Each _str_SupportedCodec As String In _str_SupportedCodecs
                If _str_SupportedCodec.Contains("H264") Then
                    ' H264 is supported? Return True
                    Return True
                End If
            Next

            ' Otherwise, Return False
            Return False

        End Get
    End Property

    ' Returns whether this device supports formatting for TV
    Public ReadOnly Property Output_Video_Codec_TVOutputSupported() As Boolean
        Get
            ' Enumrate the type of device
            Select Case _str_Output_Device
                Case DEVICE_NAME_IPHONE, DEVICE_NAME_IPOD5G, DEVICE_NAME_IPODCLASSIC, DEVICE_NAME_IPODNANO, DEVICE_NAME_IPODTOUCH, DEVICE_NAME_ZUNE, DEVICE_NAME_ZUNEHD
                    Return True
            End Select

            ' Otherwise, Return False
            Return False

        End Get
    End Property

    ' Returns whether this device supports AC3 PassThrough Encoding
    Public ReadOnly Property Output_Audio_Codec_AC3PassThroughSupported() As Boolean
        Get
            Dim _str_SupportedCodecs As String() = Nothing

            ' Enumrate devices that support AC3 Audio
            Select Case _str_Output_Device
                Case DEVICE_NAME_APPLETV, DEVICE_NAME_WESTERNDIGITALDV
                    Return True
            End Select

            ' Otherwise, Return False
            Return False

        End Get
    End Property

    ' Sets / Gets whether H.264 is enabled
    Public Property Output_Video_Codec_H264Enabled() As Boolean
        Get
            Return _bln_Output_Video_Codec_H264Enabled
        End Get
        Set(ByVal value As Boolean)
            _bln_Output_Video_Codec_H264Enabled = value
        End Set
    End Property

    ' Sets / Gets whether Outputting for TV is enabled
    Public Property Output_Video_Resolution_OutputForTV() As Boolean
        Get
            Return _bln_Output_Video_Resolution_OutputForTV
        End Get
        Set(ByVal value As Boolean)
            _bln_Output_Video_Resolution_OutputForTV = value
        End Set
    End Property

    ' Sets / Gets whether H.264 is enabled
    Public Property Output_Audio_Codec_AC3PassThroughEnabled() As Boolean
        Get
            Return _bln_Output_Audio_Codec_AC3PassThroughEnabled
        End Get
        Set(ByVal value As Boolean)
            _bln_Output_Audio_Codec_AC3PassThroughEnabled = value
        End Set
    End Property

    ' Gets the H.264 Profile Level
    Public ReadOnly Property Output_Video_H264_Profile_Level() As Integer
        Get
            Return _int_Output_Video_H264_Profile_Level
        End Get
    End Property

    ' Gets whether the Low Complexity Baseline Profile should be used
    Public ReadOnly Property Output_Video_H264_Profile_LowComplexity() As Boolean
        Get
            Return _bln_Output_Video_H264_Profile_LowComplexity
        End Get
    End Property

    ' Gets the UUID that should be written for the profile
    Public ReadOnly Property Output_Video_H264_Profile_UUID() As String
        Get
            Return _str_Output_Video_H264_Profile_UUID
        End Get
    End Property

    ' Sets / Gets the Input Video Codec
    Public Property Input_Video_Codec() As String
        Get
            Return _str_Input_Video_Codec
        End Get
        Set(ByVal value As String)
            _str_Input_Video_Codec = value
        End Set
    End Property

    ' Sets / Gets the Input Video Duration
    Public Property Input_Video_Duration() As Integer
        Get
            Return _int_Input_Video_Duration
        End Get
        Set(ByVal value As Integer)
            _int_Input_Video_Duration = value
        End Set
    End Property

    ' Sets / Gets the Input Video Bitrate
    Public Property Input_Video_BitRate() As Integer
        Get
            Return _int_Input_Video_BitRate
        End Get
        Set(ByVal value As Integer)
            _int_Input_Video_BitRate = value
        End Set
    End Property

    ' Sets / Gets the Input Video Height
    Public Property Input_Video_Height() As Integer
        Get
            Return _int_Input_Video_Height
        End Get
        Set(ByVal value As Integer)
            _int_Input_Video_Height = value
        End Set
    End Property

    ' Sets / Gets the Input Video Width
    Public Property Input_Video_Width() As Integer
        Get
            Return _int_Input_Video_Width
        End Get
        Set(ByVal value As Integer)
            _int_Input_Video_Width = value
        End Set
    End Property

    ' Sets / Gets the Input Video FPS
    Public Property Input_Video_FPS() As Double
        Get
            Return _dbl_Input_Video_FPS
        End Get
        Set(ByVal value As Double)
            _dbl_Input_Video_FPS = value
        End Set
    End Property

    ' Sets / Gets the Input Video Aspect Ratio
    Public Property Input_Video_AspectRatio() As Double
        Get
            Return _dbl_Input_Video_AspectRatio
        End Get
        Set(ByVal value As Double)
            _dbl_Input_Video_AspectRatio = value
        End Set
    End Property

    ' Sets / Gets the Input Audio Stream
    Public Property Input_Audio_Stream() As Integer
        Get
            Return _int_Input_Audio_Stream
        End Get
        Set(ByVal value As Integer)
            _int_Input_Audio_Stream = value
        End Set
    End Property

    ' Sets / Gets the Input Audio Codec
    Public Property Input_Audio_Codec() As String
        Get
            Return _str_Input_Audio_Codec
        End Get
        Set(ByVal value As String)
            _str_Input_Audio_Codec = value
        End Set
    End Property

    ' Sets / Gets the Input Audio BitRate
    Public Property Input_Audio_BitRate() As Integer
        Get
            Return _int_Input_Audio_BitRate
        End Get
        Set(ByVal value As Integer)
            _int_Input_Audio_BitRate = value
        End Set
    End Property

    ' Sets / Gets the Input Audio Channels
    Public Property Input_Audio_Channels() As Integer
        Get
            Return _int_Input_Audio_Channels
        End Get
        Set(ByVal value As Integer)
            _int_Input_Audio_Channels = value
        End Set
    End Property

    ' Sets / Gets the Input Audio SampleRate
    Public Property Input_Audio_SampleRate() As Integer
        Get
            Return _int_Input_Audio_SampleRate
        End Get
        Set(ByVal value As Integer)
            _int_Input_Audio_SampleRate = value
        End Set
    End Property

    ' Returns the filtered Output Video Extension
    Public ReadOnly Property Output_Video_FileExtension() As String
        Get
            Return _str_Output_Video_FileExtension
        End Get
    End Property

    ' Returns the filtered Output Video Codec
    Public ReadOnly Property Output_Video_Codec() As String
        Get
            Return _str_Output_Video_Codec
        End Get
    End Property

    ' Returns the filtered Output Video Duration
    Public ReadOnly Property Output_Video_Duration() As Integer
        Get
            Return _int_Output_Video_Duration
        End Get
    End Property

    ' Returns the filtered Output Video BitRate
    Public ReadOnly Property Output_Video_BitRate() As Integer
        Get
            Return _int_Output_Video_BitRate
        End Get
    End Property

    ' Returns the filtered Output Video Height
    Public ReadOnly Property Output_Video_Height() As Integer
        Get
            Return _int_Output_Video_Height
        End Get
    End Property

    ' Returns the filtered Output Video Width
    Public ReadOnly Property Output_Video_Width() As Integer
        Get
            Return _int_Output_Video_Width
        End Get
    End Property

    ' Returns the filtered Output Video Top Padding
    Public ReadOnly Property Output_Video_Padding_Top() As Integer
        Get
            Return _int_Output_Video_Padding_Top
        End Get
    End Property

    ' Returns the filtered Output Video Bottom Padding
    Public ReadOnly Property Output_Video_Padding_Bottom() As Integer
        Get
            Return _int_Output_Video_Padding_Bottom
        End Get
    End Property

    ' Returns the filtered Output Video FPS
    Public ReadOnly Property Output_Video_FPS() As Double
        Get
            Return _dbl_Output_Video_FPS
        End Get
    End Property

    ' Returns the filtered Output Audio Stream
    Public ReadOnly Property Output_Audio_Stream() As Integer
        Get
            Return _int_Output_Audio_Stream
        End Get
    End Property

    ' Returns the filtered Output Audio Codecs
    Public ReadOnly Property Output_Audio_Codecs() As String()
        Get
            Return _str_Output_Audio_Codecs
        End Get
    End Property

    ' Returns the filtered Output Audio BitRate
    Public ReadOnly Property Output_Audio_BitRate() As Integer
        Get
            Return _int_Output_Audio_BitRate
        End Get
    End Property

    ' Returns the filtered Output Audio Channels
    Public ReadOnly Property Output_Audio_Channels() As Integer
        Get
            Return _int_Output_Audio_Channels
        End Get
    End Property

    ' Returns the filtered Output Audio Sample Rate
    Public ReadOnly Property Output_Audio_SampleRate() As Integer
        Get
            Return _int_Output_Audio_SampleRate
        End Get
    End Property

#End Region

    ' Sets up the Output Parameters based on the known limitations of each device
    ' and information taken from the input file
    Public Sub ConfigureOutputParameters()

        sub_DebugMessage()

        ' Check the device type and assign the relevant profile info
        Dim _stc_EncodingSpecifications As EncodingSpecifications = _func_ConfigureDeviceSpecifications()

        ' Set up the Output video specifications in a separate structure
        Dim _stc_EncodingVideoOutput As New EncodingVideoOutput

        ' Set our initial Output values
        _int_Output_Video_Duration = _int_Input_Video_Duration
        _dbl_Output_Video_FPS = _dbl_Input_Video_FPS
        _int_Output_Video_BitRate = _int_Input_Video_BitRate
        _int_Output_Video_Height = _int_Input_Video_Height
        _int_Output_Video_Width = _int_Input_Video_Width
        _int_Output_Audio_Stream = _int_Input_Audio_Stream
        _int_Output_Audio_Channels = _int_Input_Audio_Channels
        _int_Output_Audio_SampleRate = _int_Input_Audio_SampleRate
        _int_Output_Audio_BitRate = _int_Input_Audio_BitRate

        If _stc_EncodingSpecifications.VIDEO_FILE_EXTENSION = Nothing Then
            _str_Output_Video_FileExtension = OUTPUT_FILEEXTENSION_STANDARD
        Else
            _str_Output_Video_FileExtension = _stc_EncodingSpecifications.VIDEO_FILE_EXTENSION
        End If
        sub_DebugMessage("Output File Format Extension: " & _str_Output_Video_FileExtension)

        ' ***** Check if H.264 Encoding is enabled and adjust our options accordingly *****
        If _bln_Output_Video_Codec_H264Enabled Then

            sub_DebugMessage("H.264 has been enabled")

            ' Check if we've got a forced FPS for the specific device, and if so, adjust the framerate
            If _stc_EncodingSpecifications.VIDEO_H264_FPS_FORCED <> 0 Then
                sub_DebugMessage("Specification requires a forced FrameRate")
                _dbl_Output_Video_FPS = _stc_EncodingSpecifications.VIDEO_H264_FPS_FORCED
            End If

            ' Check if 24 FPS specifics are required
            If Not _stc_EncodingSpecifications.VIDEO_H264_FPS_24_RESOLUTION_HEIGHT_MAX = 0 And Math.Round(_dbl_Input_Video_FPS, 2) = 24 Then
                sub_DebugMessage("Setting resolution limitations for 24 FPS video...")
                _stc_EncodingVideoOutput.VIDEO_RESOLUTION_HEIGHT_MAX = _stc_EncodingSpecifications.VIDEO_H264_FPS_24_RESOLUTION_HEIGHT_MAX
                _stc_EncodingVideoOutput.VIDEO_RESOLUTION_WIDTH_MAX = _stc_EncodingSpecifications.VIDEO_H264_FPS_24_RESOLUTION_WIDTH_MAX
                ' Check if 25 FPS specifics are required
            ElseIf Not _stc_EncodingSpecifications.VIDEO_H264_FPS_25_RESOLUTION_HEIGHT_MAX = 0 Then
                sub_DebugMessage("Setting resolution limitations for 25 FPS video...")
                _stc_EncodingVideoOutput.VIDEO_RESOLUTION_HEIGHT_MAX = _stc_EncodingSpecifications.VIDEO_H264_FPS_25_RESOLUTION_HEIGHT_MAX
                _stc_EncodingVideoOutput.VIDEO_RESOLUTION_WIDTH_MAX = _stc_EncodingSpecifications.VIDEO_H264_FPS_25_RESOLUTION_WIDTH_MAX
            Else
                ' Otherwise, stick with the 30 FPS defaults
                _stc_EncodingVideoOutput.VIDEO_RESOLUTION_HEIGHT_MAX = _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX
                _stc_EncodingVideoOutput.VIDEO_RESOLUTION_WIDTH_MAX = _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX
            End If

            ' Set the Maximum Bitrate
            _stc_EncodingVideoOutput.VIDEO_BITRATE_MAX = _stc_EncodingSpecifications.VIDEO_H264_BITRATE_MAX

            ' Check Input Video BitRate against the Maximum Video BitRate and reduce if required
            If _int_Input_Video_BitRate > _stc_EncodingSpecifications.VIDEO_H264_BITRATE_MAX Then
                sub_DebugMessage("Adjusting Video BitRate to: " & _stc_EncodingSpecifications.VIDEO_H264_BITRATE_MAX)
                _int_Output_Video_BitRate = _stc_EncodingSpecifications.VIDEO_H264_BITRATE_MAX - _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX
            End If

            ' Set up H.264 Profile Level
            _int_Output_Video_H264_Profile_Level = _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LEVEL
            ' Set up wther H.264 Profile Low Complexity Baseline is required
            _bln_Output_Video_H264_Profile_LowComplexity = _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LOWCOMPLEXITY

            ' ***** Otherwise, stick to MPEG4 encoding *****
        Else

            ' Check if we've got a forced FPS for the specific device, and if so, adjust the framerate
            If _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_FORCED <> 0 Then
                sub_DebugMessage("Specification requires a forced FrameRate")
                _dbl_Output_Video_FPS = _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_FORCED
            End If

            ' Check if 24 FPS specifics are required
            If Not _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_24_RESOLUTION_HEIGHT_MAX = 0 And Math.Round(_dbl_Input_Video_FPS, 2) = 24 Then
                sub_DebugMessage("Setting resolution limitations for 24 FPS video...")
                _stc_EncodingVideoOutput.VIDEO_RESOLUTION_HEIGHT_MAX = _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_24_RESOLUTION_HEIGHT_MAX
                _stc_EncodingVideoOutput.VIDEO_RESOLUTION_WIDTH_MAX = _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_24_RESOLUTION_WIDTH_MAX
                ' Check if 25 FPS specifics are required
            ElseIf Not _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_HEIGHT_MAX = 0 Then
                sub_DebugMessage("Setting resolution limitations for 25 FPS video...")
                _stc_EncodingVideoOutput.VIDEO_RESOLUTION_HEIGHT_MAX = _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_HEIGHT_MAX
                _stc_EncodingVideoOutput.VIDEO_RESOLUTION_WIDTH_MAX = _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_WIDTH_MAX
            Else
                ' Otherwise, stick with the 30 FPS defaults
                _stc_EncodingVideoOutput.VIDEO_RESOLUTION_HEIGHT_MAX = _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX
                _stc_EncodingVideoOutput.VIDEO_RESOLUTION_WIDTH_MAX = _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX
            End If

            ' Set the Maximum Bitrate
            _stc_EncodingVideoOutput.VIDEO_BITRATE_MAX = _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX

            ' Check Input Video BitRate against the Maximum Video BitRate and reduce if required
            If _int_Input_Video_BitRate > _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX Then
                sub_DebugMessage("Adjusting Video BitRate to: " & _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX - _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX)
                _int_Output_Video_BitRate = _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX - _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX
            End If

        End If

        ' Check Input FPS against our maximum and reduce if necessary
        If _dbl_Input_Video_FPS > 30 Then _dbl_Output_Video_FPS = 30

        ' Set video scaling for this device (if video is too large)
        If _int_Input_Video_Height > _stc_EncodingVideoOutput.VIDEO_RESOLUTION_HEIGHT_MAX Or _int_Input_Video_Width > _stc_EncodingVideoOutput.VIDEO_RESOLUTION_WIDTH_MAX Then
            sub_DebugMessage("Device Profile Resolution Limit: " & _stc_EncodingVideoOutput.VIDEO_RESOLUTION_HEIGHT_MAX & "x" & _stc_EncodingVideoOutput.VIDEO_RESOLUTION_WIDTH_MAX)
            Dim _int_VideoHeightDivisor As Double = CDbl(_int_Input_Video_Height / _stc_EncodingVideoOutput.VIDEO_RESOLUTION_HEIGHT_MAX)
            Dim _int_VideoWidthDivisor As Double = CDbl(_int_Input_Video_Width / _stc_EncodingVideoOutput.VIDEO_RESOLUTION_WIDTH_MAX)
            Dim _dbl_OutputAspectRatio As Double = CDbl(_stc_EncodingVideoOutput.VIDEO_RESOLUTION_WIDTH_MAX / _stc_EncodingVideoOutput.VIDEO_RESOLUTION_HEIGHT_MAX)
            Dim _dbl_OutputVideoScale As Double = Nothing

            ' Divide by the lowest value
            If _int_VideoHeightDivisor > _int_VideoWidthDivisor Then
                _dbl_OutputVideoScale = _int_VideoHeightDivisor
            Else
                _dbl_OutputVideoScale = _int_VideoWidthDivisor
            End If

            Dim _int_VideoHalfWidth As Integer = CInt(_int_Input_Video_Width / 2)
            Dim _int_VideoHalfHeight As Integer = CInt(_int_Input_Video_Height / 2)
            _int_Output_Video_Width = CInt(2 * (Math.Round(_int_VideoHalfWidth / _dbl_OutputVideoScale)))
            _int_Output_Video_Height = CInt(2 * (Math.Round(_int_VideoHalfHeight / _dbl_OutputVideoScale)))
            'Dim _dbl_OutputAspectRatio As Double = CDbl(_stc_EncodingVideoOutput.VIDEO_RESOLUTION_WIDTH_MAX / _stc_EncodingVideoOutput.VIDEO_RESOLUTION_HEIGHT_MAX)
            'Dim _dbl_OutputVideoScale As Double = Nothing

            'If _dbl_OutputAspectRatio > _dbl_Input_Video_AspectRatio Then
            '    _dbl_OutputVideoScale = _stc_EncodingVideoOutput.VIDEO_RESOLUTION_HEIGHT_MAX / _int_Input_Video_Height
            'Else
            '    _dbl_OutputVideoScale = _stc_EncodingVideoOutput.VIDEO_RESOLUTION_WIDTH_MAX / _int_Input_Video_Width
            'End If

            '_int_Output_Video_Width = CInt(2 * (Math.Round(_int_VideoHalfWidth * _dbl_OutputVideoScale)))
            '_int_Output_Video_Height = CInt(2 * (Math.Round(_int_VideoHalfHeight * _dbl_OutputVideoScale)))
            sub_DebugMessage("Scaled Output Resolution : " & _int_Output_Video_Height & "x" & _int_Output_Video_Width)
        End If

        '' Check Input Video Resolution against the Maximum Video Resolution and scale (in proportion) if required
        'If _int_Input_Video_Width > _stc_EncodingVideoOutput.VIDEO_RESOLUTION_WIDTH_MAX Then
        '    _int_Output_Video_Width = _stc_EncodingVideoOutput.VIDEO_RESOLUTION_WIDTH_MAX
        '    _int_Output_Video_Height = CInt(Math.Round(_int_Input_Video_Height / _int_Input_Video_Width, 1) * _stc_EncodingVideoOutput.VIDEO_RESOLUTION_HEIGHT_MAX)
        '    sub_DebugMessage("Adjusting Video Resolution to format height restrictions (" & _int_Output_Video_Height & "x" & _int_Output_Video_Width & ")...")
        '    'ElseIf _int_Input_Video_Height > _stc_EncodingVideoOutput.VIDEO_RESOLUTION_HEIGHT_MAX Then
        '    '    _int_Output_Video_Height = _stc_EncodingVideoOutput.VIDEO_RESOLUTION_HEIGHT_MAX
        '    '    _int_Input_Video_Height = CInt(Math.Round(_int_Input_Video_Width / _int_Input_Video_Height, 1) * _stc_EncodingVideoOutput.VIDEO_RESOLUTION_WIDTH_MAX)
        '    '    sub_DebugMessage("Adjusting Video Resolution to format width restrictions (" & _int_Output_Video_Height & "x" & _int_Output_Video_Width & ")...")
        'End If

        ' If audio is found to encode...
        If Not _int_Input_Audio_Stream = 99 Then

            ' Check Audio Codec and determine whether AC3 passthrough should be used (if supported and enabled)
            If _str_Input_Audio_Codec = "AC3" And _bln_Output_Audio_Codec_AC3PassThroughEnabled Then

                sub_DebugMessage("AC3 Audio detected and AC3 Passthrough has been enabled")

                ' Change our standard output file type
                If Not _stc_EncodingSpecifications.VIDEO_FILE_EXTENSION = OUTPUT_FILEEXTENSION_AC3PASSTHROUGH Then
                    sub_DebugMessage("Changing default file extension to " & OUTPUT_FILEEXTENSION_AC3PASSTHROUGH)
                    _str_Output_Video_FileExtension = OUTPUT_FILEEXTENSION_AC3PASSTHROUGH
                End If

                ' Set up whether AC3 only or AC3 + AAC
                If _stc_EncodingSpecifications.AUDIO_AC3_OUTPUT_ONLY Then
                    Dim _str_AudioCodecTemp As String() = {"AC3"}
                    _str_Output_Audio_Codecs = _str_AudioCodecTemp
                Else
                    Dim _str_AudioCodecTemp As String() = {"AAC", "AC3"}
                    _str_Output_Audio_Codecs = _str_AudioCodecTemp
                End If

                ' Check Input Audio BitRate against the Maximum Audio BitRate and reduce if required
                If _int_Input_Audio_BitRate > _stc_EncodingSpecifications.AUDIO_AC3_BITRATE_MAX Then
                    sub_DebugMessage("Adjusting Audio BitRate to: " & _stc_EncodingSpecifications.AUDIO_AC3_BITRATE_MAX)
                    _int_Output_Audio_BitRate = _stc_EncodingSpecifications.AUDIO_AC3_BITRATE_MAX
                End If

                ' Check Input Audio Sample Rate against the Maximum Audio Sample Rate and reduce if required
                If _int_Input_Audio_SampleRate > _stc_EncodingSpecifications.AUDIO_AC3_SAMPLERATE_MAX Then
                    sub_DebugMessage("Adjusting Audio Sample Rate to: " & _stc_EncodingSpecifications.AUDIO_AC3_SAMPLERATE_MAX)
                    _int_Output_Audio_SampleRate = _stc_EncodingSpecifications.AUDIO_AC3_SAMPLERATE_MAX
                End If

            Else

                Dim _str_AudioCodecTemp As String() = {"AAC"}
                _str_Output_Audio_Codecs = _str_AudioCodecTemp

                ' Check Input Audio BitRate against the Maximum Audio BitRate and reduce if required
                If _int_Input_Audio_BitRate > _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX Then
                    sub_DebugMessage("Adjusting Audio BitRate to: " & _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX)
                    _int_Output_Audio_BitRate = _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX
                ElseIf _int_Input_Audio_BitRate < _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX And _str_Input_Audio_Codec = "AAC" Then
                    ' Force the audio stream to be copied if the input audio is lower than the max bitrate AND it's AAC audio already
                    _int_Output_Audio_BitRate = 0
                End If

                ' Check Input Audio Sample Rate against the Maximum Audio Sample Rate and reduce if required
                If _int_Input_Audio_SampleRate > _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX Then
                    sub_DebugMessage("Adjusting Audio Sample Rate to: " & _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX)
                    _int_Output_Audio_SampleRate = _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX
                End If

            End If
        End If

        ' Check if all of the input video settings are the same as the output
        Dim _bln_Output_Video_Copy As Boolean = False
        'If _int_Input_Video_BitRate = _int_Output_Video_BitRate And _
        '    _int_Input_Video_Height = _int_Output_Video_Height And _
        '    _int_Input_Video_Width = _int_Output_Video_Width And _
        '    _dbl_Input_Video_FPS = _dbl_Output_Video_FPS Then
        '    ' Check the codecs to see if they match
        '    If (_bln_Output_Video_Codec_H264Enabled And _str_Input_Video_Codec.ToUpper.Contains("AVC")) Or _
        '        (Not _bln_Output_Video_Codec_H264Enabled And _str_Input_Video_Codec = "MPEG4") Then
        '        sub_DebugMessage("The input video is the same as the intended output. The video stream will be copied from the source file")
        '        _bln_Output_Video_Copy = True
        '    End If
        'End If

        If _bln_Output_Video_Copy Then
            _str_Output_Video_Codec = "COPY"
        ElseIf _bln_Output_Video_Codec_H264Enabled Then
            _str_Output_Video_Codec = "H264"
        Else
            _str_Output_Video_Codec = "MPEG4"
        End If

    End Sub

    Private Function _func_ConfigureDeviceSpecifications() As EncodingSpecifications

        sub_DebugMessage("Configuring specifications for device: " & _str_Output_Device & "...")

        Dim _stc_EncodingSpecifications As New EncodingSpecifications

        ' Enumrate the type of device
        Select Case _str_Output_Device
            Case DEVICE_NAME_APPLETV

                _stc_EncodingSpecifications.VIDEO_FILE_EXTENSION = "M4V"

                _stc_EncodingSpecifications.VIDEO_H264_FPS_FORCED = 25
                _stc_EncodingSpecifications.VIDEO_H264_FPS_25_RESOLUTION_WIDTH_MAX = 1280
                _stc_EncodingSpecifications.VIDEO_H264_FPS_25_RESOLUTION_HEIGHT_MAX = 720
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_FORCED = 25
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_WIDTH_MAX = 720
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_HEIGHT_MAX = 432

                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LEVEL = 31
                _stc_EncodingSpecifications.VIDEO_H264_BITRATE_MAX = 5000
                _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX = 3000

                _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX = 160
                _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX = 48000
                _stc_EncodingSpecifications.AUDIO_AAC_CHANNELS_MAX = 2

                _stc_EncodingSpecifications.AUDIO_AC3_BITRATE_MAX = 160
                _stc_EncodingSpecifications.AUDIO_AC3_SAMPLERATE_MAX = 48000
                _stc_EncodingSpecifications.AUDIO_AC3_CHANNELS_MAX = 6

            Case DEVICE_NAME_BLACKBERRY9500, DEVICE_NAME_BLACKBERRY9000

                _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX = 480
                _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX = 320
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX = 480
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX = 320

                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LEVEL = 30
                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LOWCOMPLEXITY = True
                _stc_EncodingSpecifications.VIDEO_H264_BITRATE_MAX = 800
                _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX = 400

                _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX = 128
                _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX = 44100
                _stc_EncodingSpecifications.AUDIO_AAC_CHANNELS_MAX = 2

            Case DEVICE_NAME_BLACKBERRY8900, DEVICE_NAME_BLACKBERRY8800, DEVICE_NAME_BLACKBERRY8700

                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_FORCED = 25
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_WIDTH_MAX = 320
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_HEIGHT_MAX = 240

                _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX = 800

                _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX = 128
                _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX = 44100
                _stc_EncodingSpecifications.AUDIO_AAC_CHANNELS_MAX = 2

            Case DEVICE_NAME_BLACKBERRY8300, DEVICE_NAME_BLACKBERRY8200, DEVICE_NAME_BLACKBERRY8100

                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_FORCED = 25
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_WIDTH_MAX = 320
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_HEIGHT_MAX = 240

                _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX = 400

                _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX = 64
                _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX = 44100
                _stc_EncodingSpecifications.AUDIO_AAC_CHANNELS_MAX = 2

            Case DEVICE_NAME_IPHONE, DEVICE_NAME_IPODTOUCH

                _stc_EncodingSpecifications.VIDEO_FILE_EXTENSION = "M4V"

                If _bln_Output_Video_Resolution_OutputForTV Then
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX = 640
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX = 480
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX = 640
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX = 480
                Else
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX = 480
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX = 320
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX = 480
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX = 320
                End If

                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LEVEL = 30
                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LOWCOMPLEXITY = True
                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_UUID = "1200"
                _stc_EncodingSpecifications.VIDEO_H264_BITRATE_MAX = 1500
                _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX = 2500

                _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX = 160
                _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX = 48000
                _stc_EncodingSpecifications.AUDIO_AAC_CHANNELS_MAX = 2

            Case DEVICE_NAME_IPOD5G, DEVICE_NAME_IPODCLASSIC, DEVICE_NAME_IPODNANO

                _stc_EncodingSpecifications.VIDEO_FILE_EXTENSION = "M4V"

                If _bln_Output_Video_Resolution_OutputForTV Then
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX = 640
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX = 480
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX = 640
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX = 480
                Else
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX = 320
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX = 240
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX = 320
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX = 240
                End If

                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LEVEL = 30
                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LOWCOMPLEXITY = True
                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_UUID = "1200"
                _stc_EncodingSpecifications.VIDEO_H264_BITRATE_MAX = 1500
                _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX = 2500

                _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX = 160
                _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX = 48000
                _stc_EncodingSpecifications.AUDIO_AAC_CHANNELS_MAX = 2

            Case DEVICE_NAME_NOKIAE71

                _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX = 320
                _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX = 240
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX = 320
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX = 240

                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LEVEL = 21
                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LOWCOMPLEXITY = True
                _stc_EncodingSpecifications.VIDEO_H264_BITRATE_MAX = 384
                _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX = 384

                _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX = 112
                _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX = 44100
                _stc_EncodingSpecifications.AUDIO_AAC_CHANNELS_MAX = 2

            Case DEVICE_NAME_PLAYSTATION3, DEVICE_NAME_XBOX360

                _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX = 1920
                _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX = 1280
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX = 1280
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX = 720

                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LEVEL = 41
                _stc_EncodingSpecifications.VIDEO_H264_BITRATE_MAX = 10000
                _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX = 5000

                _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX = 160
                _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX = 48000
                _stc_EncodingSpecifications.AUDIO_AAC_CHANNELS_MAX = 2

            Case DEVICE_NAME_PSP

                _stc_EncodingSpecifications.VIDEO_H264_AR_PADDING = True
                _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX = 480
                _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX = 272
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX = 480
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX = 272

                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LEVEL = 21
                _stc_EncodingSpecifications.VIDEO_H264_BITRATE_MAX = 768
                _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX = 640

                _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX = 128
                _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX = 48000
                _stc_EncodingSpecifications.AUDIO_AAC_CHANNELS_MAX = 2

            Case DEVICE_NAME_TMOBILEG1

                _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX = 480
                _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX = 368
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX = 480
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX = 368

                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LEVEL = 30
                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LOWCOMPLEXITY = True
                _stc_EncodingSpecifications.VIDEO_H264_BITRATE_MAX = 1500
                _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX = 2500

                _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX = 96
                _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX = 44100
                _stc_EncodingSpecifications.AUDIO_AAC_CHANNELS_MAX = 2

            Case DEVICE_NAME_WESTERNDIGITALDV

                _stc_EncodingSpecifications.VIDEO_FILE_EXTENSION = "M4V"

                _stc_EncodingSpecifications.VIDEO_H264_FPS_24_RESOLUTION_WIDTH_MAX = 1920
                _stc_EncodingSpecifications.VIDEO_H264_FPS_24_RESOLUTION_HEIGHT_MAX = 1080
                _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX = 1980
                _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX = 1080
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX = 1980
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX = 1080

                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LEVEL = 50
                _stc_EncodingSpecifications.VIDEO_H264_BITRATE_MAX = 10000
                _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX = 5000

                _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX = 160
                _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX = 48000
                _stc_EncodingSpecifications.AUDIO_AAC_CHANNELS_MAX = 2

                _stc_EncodingSpecifications.AUDIO_AC3_OUTPUT_ONLY = True
                _stc_EncodingSpecifications.AUDIO_AC3_BITRATE_MAX = 320
                _stc_EncodingSpecifications.AUDIO_AC3_SAMPLERATE_MAX = 48000
                _stc_EncodingSpecifications.AUDIO_AC3_CHANNELS_MAX = 6

            Case DEVICE_NAME_YOUTUBEHD

                _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX = 1280
                _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX = 720
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX = 1280
                _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX = 720

                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LEVEL = 51
                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LOWCOMPLEXITY = True
                _stc_EncodingSpecifications.VIDEO_H264_BITRATE_MAX = 1500
                _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX = 2500

                _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX = 320
                _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX = 48000
                _stc_EncodingSpecifications.AUDIO_AAC_CHANNELS_MAX = 2

            Case DEVICE_NAME_ZUNE

                If _bln_Output_Video_Resolution_OutputForTV Then
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_25_RESOLUTION_WIDTH_MAX = 720
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_25_RESOLUTION_HEIGHT_MAX = 576
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX = 720
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX = 480
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_WIDTH_MAX = 720
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_HEIGHT_MAX = 576
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX = 720
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX = 480
                Else
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_25_RESOLUTION_WIDTH_MAX = 320
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_25_RESOLUTION_HEIGHT_MAX = 240
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX = 320
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX = 240
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_WIDTH_MAX = 320
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_HEIGHT_MAX = 240
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX = 320
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX = 240
                End If

                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LEVEL = 21
                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LOWCOMPLEXITY = True
                _stc_EncodingSpecifications.VIDEO_H264_BITRATE_MAX = 2500
                _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX = 2500

                _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX = 192
                _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX = 48000
                _stc_EncodingSpecifications.AUDIO_AAC_CHANNELS_MAX = 2

            Case DEVICE_NAME_ZUNEHD

                If _bln_Output_Video_Resolution_OutputForTV Then
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_25_RESOLUTION_WIDTH_MAX = 1280
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_25_RESOLUTION_HEIGHT_MAX = 720
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX = 1280
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX = 720
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_WIDTH_MAX = 720
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_HEIGHT_MAX = 576
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX = 720
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX = 480
                Else
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_25_RESOLUTION_WIDTH_MAX = 480
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_25_RESOLUTION_HEIGHT_MAX = 272
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_WIDTH_MAX = 480
                    _stc_EncodingSpecifications.VIDEO_H264_FPS_30_RESOLUTION_HEIGHT_MAX = 272
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_WIDTH_MAX = 480
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_25_RESOLUTION_HEIGHT_MAX = 272
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_WIDTH_MAX = 480
                    _stc_EncodingSpecifications.VIDEO_MPEG4_FPS_30_RESOLUTION_HEIGHT_MAX = 272
                End If

                _stc_EncodingSpecifications.VIDEO_H264_PROFILE_LEVEL = 31
                _stc_EncodingSpecifications.VIDEO_H264_BITRATE_MAX = 10000
                _stc_EncodingSpecifications.VIDEO_MPEG4_BITRATE_MAX = 4000

                _stc_EncodingSpecifications.AUDIO_AAC_BITRATE_MAX = 320
                _stc_EncodingSpecifications.AUDIO_AAC_SAMPLERATE_MAX = 48000
                _stc_EncodingSpecifications.AUDIO_AAC_CHANNELS_MAX = 2

        End Select

        Return _stc_EncodingSpecifications

    End Function

End Class



﻿using System;
using System.Linq;
using PepperDash.Core;
using PepperDash.Essentials.Core.Queues;

namespace SonyBraviaEpi
{
    public static class Commands
    {
        public static readonly byte[] PowerOn = {0x8c, 0x00, 0x00, 0x02, 0x01};
        public static readonly byte[] PowerOff = {0x8c, 0x00, 0x00, 0x02, 0x00};
        public static readonly byte[] PowerQuery = {0x83, 0x00, 0x00, 0xFF, 0xFF};
        public static readonly byte[] InputVideo1 = {0x8C, 0x00, 0x02, 0x03, 0x02, 0x01};
        public static readonly byte[] InputVideo2 = {0x8C, 0x00, 0x02, 0x03, 0x02, 0x02};
        public static readonly byte[] InputVideo3 = {0x8C, 0x00, 0x02, 0x03, 0x02, 0x03};
        public static readonly byte[] InputComponent1 = {0x8C, 0x00, 0x02, 0x03, 0x03, 0x01};
        public static readonly byte[] InputComponent2 = {0x8C, 0x00, 0x02, 0x03, 0x03, 0x02};
        public static readonly byte[] InputComponent3 = {0x8C, 0x00, 0x02, 0x03, 0x03, 0x03};
        public static readonly byte[] InputHdmi1 = {0x8C, 0x00, 0x02, 0x03, 0x04, 0x01};
        public static readonly byte[] InputHdmi2 = {0x8C, 0x00, 0x02, 0x03, 0x04, 0x02};
        public static readonly byte[] InputHdmi3 = {0x8C, 0x00, 0x02, 0x03, 0x04, 0x03};
        public static readonly byte[] InputHdmi4 = {0x8C, 0x00, 0x02, 0x03, 0x04, 0x04};
        public static readonly byte[] InputHdmi5 = {0x8C, 0x00, 0x02, 0x03, 0x04, 0x05};
        public static readonly byte[] InputPc1 = {0x8C, 0x00, 0x02, 0x03, 0x05, 0x01};
        public static readonly byte[] InputQuery = {0x83, 0x00, 0x02, 0xFF, 0xFF};

        public static byte CalculateChecksum(this byte[] data)
        {
            var result = data.Aggregate(0x00, (current, b) => current + b);
            return Convert.ToByte(result > 0xff ? data[data.Length - 1] : result);
        }

        public static byte[] WithChecksum(this byte[] data)
        {
            var checksum = data.CalculateChecksum();
            var newArray = data.ToList();
            newArray.Add(checksum);
            return newArray.ToArray();
        }

        public static IQueueMessage GetPowerOn(IBasicCommunication coms)
        {
            return new ComsMessage(coms, PowerOn.WithChecksum());
        }

        public static IQueueMessage GetPowerOff(IBasicCommunication coms)
        {
            return new ComsMessage(coms, PowerOff.WithChecksum());
        }

        public static IQueueMessage GetPowerQuery(IBasicCommunication coms)
        {
            return new ComsMessage(coms, PowerQuery.WithChecksum());
        }

        public static IQueueMessage GetHdmi1(IBasicCommunication coms)
        {
            return new ComsMessage(coms, InputHdmi1.WithChecksum());
        }

        public static IQueueMessage GetHdmi2(IBasicCommunication coms)
        {
            return new ComsMessage(coms, InputHdmi2.WithChecksum());
        }

        public static IQueueMessage GetHdmi3(IBasicCommunication coms)
        {
            return new ComsMessage(coms, InputHdmi3.WithChecksum());
        }

        public static IQueueMessage GetHdmi4(IBasicCommunication coms)
        {
            return new ComsMessage(coms, InputHdmi4.WithChecksum());
        }

        public static IQueueMessage GetHdmi5(IBasicCommunication coms)
        {
            return new ComsMessage(coms, InputHdmi5.WithChecksum());
        }

        public static IQueueMessage GetVideo1(IBasicCommunication coms)
        {
            return new ComsMessage(coms, InputVideo1.WithChecksum());
        }

        public static IQueueMessage GetVideo2(IBasicCommunication coms)
        {
            return new ComsMessage(coms, InputVideo2.WithChecksum());
        }

        public static IQueueMessage GetVideo3(IBasicCommunication coms)
        {
            return new ComsMessage(coms, InputVideo3.WithChecksum());
        }

        public static IQueueMessage GetComponent1(IBasicCommunication coms)
        {
            return new ComsMessage(coms, InputComponent1.WithChecksum());
        }

        public static IQueueMessage GetComponent2(IBasicCommunication coms)
        {
            return new ComsMessage(coms, InputComponent2.WithChecksum());
        }

        public static IQueueMessage GetComponent3(IBasicCommunication coms)
        {
            return new ComsMessage(coms, InputComponent3.WithChecksum());
        }

        public static IQueueMessage GetPc(IBasicCommunication coms)
        {
            return new ComsMessage(coms, InputPc1.WithChecksum());
        }

        public static IQueueMessage GetInputQuery(IBasicCommunication coms)
        {
            return new ComsMessage(coms, InputQuery.WithChecksum());
        }
        /*const commands = {
  "power_on":                        [0x8C, 0x00, 0x00, 0x02, 0x01],
  "power_off":                       [0x8C, 0x00, 0x00, 0x02, 0x00],
  "power_get":                       [0x83, 0x00, 0x00, 0xFF, 0xFF],
  "standby_on":                      [0x8C, 0x00, 0x01, 0x02, 0x01],
  "standby_off":                     [0x8C, 0x00, 0x01, 0x02, 0x00],
  "input_select_toggle":             [0x8C, 0x00, 0x02, 0x02, 0x00],
  "input_select_video1":             [0x8C, 0x00, 0x02, 0x03, 0x02, 0x01],
  "input_select_video2":             [0x8C, 0x00, 0x02, 0x03, 0x02, 0x02],
  "input_select_video2":             [0x8C, 0x00, 0x02, 0x03, 0x02, 0x03],
  "input_select_component1":         [0x8C, 0x00, 0x02, 0x03, 0x03, 0x01],
  "input_select_component2":         [0x8C, 0x00, 0x02, 0x03, 0x03, 0x02],
  "input_select_component3":         [0x8C, 0x00, 0x02, 0x03, 0x03, 0x03],
  "input_select_hdmi1":              [0x8C, 0x00, 0x02, 0x03, 0x04, 0x01],
  "input_select_hdmi2":              [0x8C, 0x00, 0x02, 0x03, 0x04, 0x02],
  "input_select_hdmi3":              [0x8C, 0x00, 0x02, 0x03, 0x04, 0x03],
  "input_select_hdmi4":              [0x8C, 0x00, 0x02, 0x03, 0x04, 0x04],
  "input_select_hdmi5":              [0x8C, 0x00, 0x02, 0x03, 0x04, 0x05],
  "input_select_pc1":                [0x8C, 0x00, 0x02, 0x03, 0x05, 0x01],
  "input_select_shared1":            [0x8C, 0x00, 0x02, 0x03, 0x07, 0x01],
  "input_select_get":                [0x83, 0x00, 0x02, 0xFF, 0xFF],
  "volume_control_up":               [0x8C, 0x00, 0x05, 0x03, 0x00, 0x00],
  "volume_control_down":             [0x8C, 0x00, 0x05, 0x03, 0x00, 0x01],
  "volume_control_direct":           [0x8C, 0x00, 0x05, 0x03, 0x01, 0x00],
  "muting_toggle":                   [0x8C, 0x00, 0x06, 0x02, 0x00],
  "muting_off":                      [0x8C, 0x00, 0x06, 0x03, 0x01, 0x00],
  "muting_on":                       [0x8C, 0x00, 0x06, 0x03, 0x01, 0x01],
  "off_timer_toggle":                [0x8C, 0x00, 0x0C, 0x02, 0x00],
  "off_timer_direct":                [0x8C, 0x00, 0x0C, 0x03, 0x01, 0x00],
  "picture_on":                      [0x8C, 0x00, 0x0D, 0x03, 0x01, 0x01],
  "picture_off":                     [0x8C, 0x00, 0x0D, 0x03, 0x01, 0x00],
  "teletext_tottle":                 [0x8C, 0x00, 0x0E, 0x02, 0x00],
  "teletext_direct_off":             [0x8C, 0x00, 0x0E, 0x03, 0x01, 0x00],
  "teletext_direct_text":            [0x8C, 0x00, 0x0E, 0x03, 0x01, 0x01],
  "teletext_direct_mix":             [0x8C, 0x00, 0x0E, 0x03, 0x01, 0x02],
  "display_toggle":                  [0x8C, 0x00, 0x0F, 0x02, 0x00],
  "closed_caption_toggle":           [0x8C, 0x00, 0x10, 0x02, 0x00],
  "closed_caption_off":              [0x8C, 0x00, 0x10, 0x03, 0x01, 0x00],
  "closed_caption_on":               [0x8C, 0x00, 0x10, 0x03, 0x01, 0x01],
  "closed_caption_analog_cc1":       [0x8C, 0x00, 0x10, 0x04, 0x02, 0x00, 0x01],
  "closed_caption_analog_cc2":       [0x8C, 0x00, 0x10, 0x04, 0x02, 0x00, 0x02],
  "closed_caption_analog_cc3":       [0x8C, 0x00, 0x10, 0x04, 0x02, 0x00, 0x03],
  "closed_caption_analog_cc4":       [0x8C, 0x00, 0x10, 0x04, 0x02, 0x00, 0x04],
  "closed_caption_analog_text1":     [0x8C, 0x00, 0x10, 0x04, 0x02, 0x00, 0x05],
  "closed_caption_analog_text2":     [0x8C, 0x00, 0x10, 0x04, 0x02, 0x00, 0x06],
  "closed_caption_analog_text3":     [0x8C, 0x00, 0x10, 0x04, 0x02, 0x00, 0x07],
  "closed_caption_analog_text4":     [0x8C, 0x00, 0x10, 0x04, 0x02, 0x00, 0x08],
  "closed_caption_digital_service1": [0x8C, 0x00, 0x10, 0x04, 0x02, 0x01, 0x01],
  "closed_caption_digital_service2": [0x8C, 0x00, 0x10, 0x04, 0x02, 0x01, 0x02],
  "closed_caption_digital_service3": [0x8C, 0x00, 0x10, 0x04, 0x02, 0x01, 0x03],
  "closed_caption_digital_service4": [0x8C, 0x00, 0x10, 0x04, 0x02, 0x01, 0x04],
  "closed_caption_digital_service5": [0x8C, 0x00, 0x10, 0x04, 0x02, 0x01, 0x05],
  "closed_caption_digital_service6": [0x8C, 0x00, 0x10, 0x04, 0x02, 0x01, 0x06],
  "closed_caption_digital_cc1":      [0x8C, 0x00, 0x10, 0x04, 0x02, 0x01, 0x07],
  "closed_caption_digital_cc2":      [0x8C, 0x00, 0x10, 0x04, 0x02, 0x01, 0x08],
  "closed_caption_digital_cc3":      [0x8C, 0x00, 0x10, 0x04, 0x02, 0x01, 0x09],
  "closed_caption_digital_cc4":      [0x8C, 0x00, 0x10, 0x04, 0x02, 0x01, 0x0A],
  "picture_mode_toggle":             [0x8C, 0x00, 0x20, 0x02, 0x00],
  "picture_mode_vivid":              [0x8C, 0x00, 0x20, 0x03, 0x01, 0x00],
  "picture_mode_standard":           [0x8C, 0x00, 0x20, 0x03, 0x01, 0x01],
  "picture_mode_cinema":             [0x8C, 0x00, 0x20, 0x03, 0x01, 0x02],
  "picture_mode_custom":             [0x8C, 0x00, 0x20, 0x03, 0x01, 0x03],
  "picture_mode_cine2":              [0x8C, 0x00, 0x20, 0x03, 0x01, 0x04],
  "picture_mode_sports":             [0x8C, 0x00, 0x20, 0x03, 0x01, 0x05],
  "picture_mode_game":               [0x8C, 0x00, 0x20, 0x03, 0x01, 0x06],
  "picture_mode_graphics":           [0x8C, 0x00, 0x20, 0x03, 0x01, 0x07],
  "sound_mode_standard":             [0x8C, 0x00, 0x30, 0x03, 0x01, 0x01],
  "sound_mode_cinema":               [0x8C, 0x00, 0x30, 0x03, 0x01, 0x04],
  "sound_mode_sports":               [0x8C, 0x00, 0x30, 0x03, 0x01, 0x05],
  "sound_mode_music":                [0x8C, 0x00, 0x30, 0x03, 0x01, 0x06],
  "sound_mode_game":                 [0x8C, 0x00, 0x30, 0x03, 0x01, 0x07],
  "speaker_toggle":                  [0x8C, 0x00, 0x36, 0x02, 0x00],
  "speaker_off":                     [0x8C, 0x00, 0x36, 0x03, 0x01, 0x00],
  "speaker_on":                      [0x8C, 0x00, 0x36, 0x03, 0x01, 0x01],
  "h_shift_up":                      [0x8C, 0x00, 0x41, 0x03, 0x00, 0x00],
  "h_shift_down":                    [0x8C, 0x00, 0x41, 0x03, 0x00, 0x01],
  "h_shift_plus":                    [0x8C, 0x00, 0x41, 0x04, 0x01, 0x00, 0x00],
  "h_shift_minus":                   [0x8C, 0x00, 0x41, 0x04, 0x01, 0x01, 0x00],
  "v_size_up":                       [0x8C, 0x00, 0x42, 0x03, 0x00, 0x00],
  "v_size_down":                     [0x8C, 0x00, 0x42, 0x03, 0x00, 0x01],
  "v_size_plus":                     [0x8C, 0x00, 0x42, 0x04, 0x01, 0x00, 0x00],
  "v_size_minus":                    [0x8C, 0x00, 0x42, 0x04, 0x01, 0x01, 0x00],
  "v_shift_up":                      [0x8C, 0x00, 0x43, 0x03, 0x00, 0x00],
  "v_shift_down":                    [0x8C, 0x00, 0x43, 0x03, 0x00, 0x01],
  "v_shift_plus":                    [0x8C, 0x00, 0x43, 0x04, 0x01, 0x00, 0x00],
  "v_shift_minus":                   [0x8C, 0x00, 0x43, 0x04, 0x01, 0x01, 0x00],
  "wide_toggle":                     [0x8C, 0x00, 0x44, 0x02, 0x00],
  "wide_widezoom":                   [0x8C, 0x00, 0x44, 0x03, 0x01, 0x00],
  "wide_full":                       [0x8C, 0x00, 0x44, 0x03, 0x01, 0x01],
  "wide_zoom":                       [0x8C, 0x00, 0x44, 0x03, 0x01, 0x02],
  "wide_normal":                     [0x8C, 0x00, 0x44, 0x03, 0x01, 0x03],
  "wide_pc_normal":                  [0x8C, 0x00, 0x44, 0x03, 0x01, 0x05],
  "wide_pc_full1":                   [0x8C, 0x00, 0x44, 0x03, 0x01, 0x06],
  "wide_pc_full2":                   [0x8C, 0x00, 0x44, 0x03, 0x01, 0x07],
  "auto_wide_toggle":                [0x8C, 0x00, 0x45, 0x02, 0x00],
  "auto_wide_off":                   [0x8C, 0x00, 0x45, 0x03, 0x01, 0x00],
  "auto_wide_on":                    [0x8C, 0x00, 0x45, 0x03, 0x01, 0x01],
  "mode43_toggle":                   [0x8C, 0x00, 0x45, 0x02, 0x00],
  "mode43_normal":                   [0x8C, 0x00, 0x45, 0x03, 0x01, 0x04],
  "mode43_widezoom":                 [0x8C, 0x00, 0x45, 0x03, 0x01, 0x03],
  "mode43_off":                      [0x8C, 0x00, 0x45, 0x03, 0x01, 0x00],
  "cinemotion_off":                  [0x8C, 0x00, 0x2A, 0x02, 0x00],
  "cinemotion_auto":                 [0x8C, 0x00, 0x2A, 0x02, 0x01],
  "picture_up":                      [0x8C, 0x00, 0x23, 0x03, 0x00, 0x00],
  "picture_down":                    [0x8C, 0x00, 0x23, 0x03, 0x00, 0x01],
  "picture_direct":                  [0x8C, 0x00, 0x23, 0x03, 0x01, 0x00],
  "brightness_up":                   [0x8C, 0x00, 0x24, 0x03, 0x00, 0x00],
  "brightness_down":                 [0x8C, 0x00, 0x24, 0x03, 0x00, 0x01],
  "brightness_direct":               [0x8C, 0x00, 0x24, 0x03, 0x01, 0x00],
  "color_up":                        [0x8C, 0x00, 0x25, 0x03, 0x00, 0x00],
  "color_down":                      [0x8C, 0x00, 0x25, 0x03, 0x00, 0x01],
  "color_direct":                    [0x8C, 0x00, 0x25, 0x03, 0x01, 0x00],
  "hue_red_up":                      [0x8C, 0x00, 0x26, 0x04, 0x00, 0x00, 0x00],
  "hue_red_down":                    [0x8C, 0x00, 0x26, 0x04, 0x00, 0x00, 0x01],
  "hue_green_up":                    [0x8C, 0x00, 0x26, 0x04, 0x00, 0x01, 0x00],
  "hue_green_down":                  [0x8C, 0x00, 0x26, 0x04, 0x00, 0x01, 0x01],
  "hue_red_direct":                  [0x8C, 0x00, 0x26, 0x04, 0x01, 0x00, 0x00],
  "hue_green_direct":                [0x8C, 0x00, 0x26, 0x04, 0x01, 0x01, 0x00],
  "sharpness_up":                    [0x8C, 0x00, 0x28, 0x03, 0x00, 0x00],
  "sharpness_down":                  [0x8C, 0x00, 0x28, 0x03, 0x00, 0x01],
  "sharpness_direct":                [0x8C, 0x00, 0x28, 0x03, 0x01, 0x00],
  "sircs_emulation":                 [0x8C, 0x00, 0x67, 0x03, 0x00, 0x00],
  "sircs_emulation_home":            [0x8C, 0x00, 0x67, 0x03, 0x01, 0x60],
  "signage_id_command":              [0x83, 0x00, 0x6F, 0xFF, 0xFF],
  "signage_productinfo1":            [0x83, 0x00, 0x6E, 0xFF, 0xFF],
  "signage_productinfo2":            [0x83, 0x00, 0x6D, 0xFF, 0xFF],
  "signage_productinfo3":            [0x83, 0x00, 0x6C, 0xFF, 0xFF]
}*/
    }
}
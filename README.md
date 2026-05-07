**🌐[ 中文 | [English](README_en.md) ]**

[📝更新日志](CHANGELOG.md)

[📦 Releases](https://github.com/JMC2002/SlayTheSpire2_SpireGuard/releases)

# SpireGuard
##  0. 安装

### Mod本体安装
Steam版本直接在创意工坊订阅即可（暂未开放）

其他版本可以自行编译，或者在[📦 Releases](https://github.com/JMC2002/SlayTheSpire2_SpireGuard/releases)界面下载.zip后解压到游戏安装目录下的Mods
目录下（没有就新建一个）

### 前置安装
**此外，本模组强依赖于模组[JmcModLib](https://github.com/JMC2002/SlayTheSpire2_JmcModLib/releases)**，安装方法同上

安装完成后的目录结构如下：

```sh
-- Slay the Spire 2
    |-- SlayTheSpire2.exe
        |-- mods
             |-- JmcModLib
             |-- SpireGuard
                  |-- SpireGuard.dll
                  |-- SpireGuard.pck
                  |-- SpireGuard.json
```

### 存档迁移
> 当你第一次安装MOD，游戏会默认将开启Mod的存档与没开启的隔离，可以按下面的方法迁移存档：

在安装好MOD后第一次打开游戏会询问是否启用MOD，启用并再次打开游戏一次后，退出游戏，将`%appdata%\SlayTheSpire2\steam\`下面的数字文件夹下的你对应的存档文件粘贴到该文件夹的`modded`文件夹中，以同步使用MOD前后的存档

---
## 🧠 1. 简介
SpireGuard 用于限制《杀戮尖塔 2》联机局中的控制台权限。默认情况下，客机无法主动打开或提交控制台命令，主机仍可正常使用控制台，并保持原版的主机命令同步行为。

[演示视频（B站）](待定)

[Github仓库](https://github.com/JMC2002/SlayTheSpire2_SpireGuard)
## ⚙️ 2. 功能
- 联机时禁止客机主动打开控制台。
- 联机时禁止客机主动提交控制台命令。
- 主机端拒收客机绕过本地限制发送的控制台网络动作。
- 提供 JmcModLib 设置项，可以随时启用或停用保护逻辑。
- 发布 manifest 标记为非玩法影响，便于作为本地联机保护工具使用。
 
## 🔔 3. 提醒
- **本模组强依赖于模组[JmcModLib](https://github.com/JMC2002/SlayTheSpire2_JmcModLib/releases)**
- 主机需要安装并启用本模组，才能权威阻止客机发来的控制台网络动作。
- 如果只有客机安装，本模组只能阻止该客机本地使用控制台，无法替未安装的主机拦截其他玩家。
 
## 🧩 4. 兼容性
- 由于游戏处于EA阶段，可能会随着游戏版本更新而失效

## 🧭 5. TODO
- 根据后续游戏版本变化继续跟进控制台和联机同步入口。

**如果你喜欢这个 Mod 的话，希望可以点一个star~**

// BreathFlow 主程式
const PHASES = {
  box: ['吸氣', '屏息', '呼氣', '停頓'],
  equal: ['吸氣', '呼氣'],
  mindful: ['吸氣', '呼氣', '停頓']
};
const DEFAULT_SECONDS = {
  box: [4, 4, 4, 4],
  equal: [4, 4],
  mindful: [4, 4, 4]
};
let timer = null, phaseIdx = 0, remain = 0, totalSec = 0, startTime = 0;
let soundOn = true, voiceOn = false;
let mode = 'box', seconds = [...DEFAULT_SECONDS.box];

const modeSelect = document.getElementById('mode');
const durationInput = document.getElementById('duration');
const startBtn = document.getElementById('start-btn');
const guideSec = document.getElementById('guide');
const phaseLabel = document.getElementById('phase-label');
const stopBtn = document.getElementById('stop-btn');
const recordList = document.getElementById('record-list');
const themeToggle = document.getElementById('theme-toggle');
const soundToggle = document.getElementById('sound-toggle');
const voiceToggle = document.getElementById('voice-toggle');
const canvas = document.getElementById('breath-canvas');
const ctx = canvas.getContext('2d');

function setTheme(dark) {
  document.body.classList.toggle('dark', dark);
  localStorage.setItem('theme', dark ? 'dark' : 'light');
}
themeToggle.onclick = () => setTheme(!document.body.classList.contains('dark'));
if (localStorage.getItem('theme') === 'dark') setTheme(true);

modeSelect.onchange = () => {
  mode = modeSelect.value;
  seconds = [...DEFAULT_SECONDS[mode]];
};

startBtn.onclick = () => {
  totalSec = parseInt(durationInput.value) * 60;
  phaseIdx = 0;
  remain = seconds[phaseIdx];
  startTime = Date.now();
  document.getElementById('mode-select').classList.add('hidden');
  guideSec.classList.remove('hidden');
  canvas.classList.add('breathing');
  nextPhase();
};

stopBtn.onclick = endPractice;

soundToggle.onclick = () => {
  soundOn = !soundOn;
  soundToggle.textContent = soundOn ? '🔊' : '🔇';
};
voiceToggle.onclick = () => {
  voiceOn = !voiceOn;
  voiceToggle.textContent = voiceOn ? '🎤' : '🚫';
};

function nextPhase() {
  if (totalSec <= 0) return endPractice();
  phaseLabel.textContent = PHASES[mode][phaseIdx];
  remain = seconds[phaseIdx];
  if (voiceOn) speak(PHASES[mode][phaseIdx]);
  animatePhase(PHASES[mode][phaseIdx], remain);
  timer = setInterval(() => {
    remain--;
    totalSec--;
    if (remain <= 0) {
      phaseIdx = (phaseIdx + 1) % PHASES[mode].length;
      clearInterval(timer);
      nextPhase();
    }
  }, 1000);
}

function endPractice() {
  clearInterval(timer);
  guideSec.classList.add('hidden');
  document.getElementById('mode-select').classList.remove('hidden');
  canvas.classList.remove('breathing');
  saveRecord({
    date: new Date().toLocaleString(),
    mode,
    duration: parseInt(durationInput.value),
    result: '完成'
  });
  loadRecords();
}

function animatePhase(phase, sec) {
  ctx.clearRect(0, 0, canvas.width, canvas.height);
  let start = Date.now();
  function draw() {
    let elapsed = (Date.now() - start) / 1000;
    let progress = Math.min(elapsed / sec, 1);
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    // 呼吸燈漸層圓形
    let radius = 100 + 30 * progress;
    let gradient = ctx.createRadialGradient(150, 150, radius * 0.5, 150, 150, radius);
    if (phase === '吸氣') {
      gradient.addColorStop(0, '#4e8cff');
      gradient.addColorStop(1, '#aeefff');
    } else if (phase === '呼氣') {
      gradient.addColorStop(0, '#aeefff');
      gradient.addColorStop(1, '#4e8cff');
    } else {
      gradient.addColorStop(0, '#4e8cff');
      gradient.addColorStop(1, '#fff2');
    }
    ctx.beginPath();
    ctx.arc(150, 150, radius, 0, 2 * Math.PI);
    ctx.fillStyle = gradient;
    ctx.globalAlpha = 0.8;
    ctx.shadowColor = '#4e8cff';
    ctx.shadowBlur = 30 + 30 * progress;
    ctx.fill();
    ctx.globalAlpha = 1;
    ctx.shadowBlur = 0;
    if (progress < 1) requestAnimationFrame(draw);
  }
  draw();
  if (soundOn) playSound(phase);
}

function playSound(phase) {
  // 簡易音效：吸氣/呼氣/停頓
  const ctx = new (window.AudioContext || window.webkitAudioContext)();
  const o = ctx.createOscillator();
  o.type = 'sine';
  o.frequency.value = phase === '吸氣' ? 440 : phase === '呼氣' ? 330 : 220;
  o.connect(ctx.destination);
  o.start();
  o.stop(ctx.currentTime + 0.3);
}

function speak(text) {
  const utter = new window.SpeechSynthesisUtterance(text);
  utter.lang = 'zh-TW';
  window.speechSynthesis.speak(utter);
}

function saveRecord(rec) {
  let records = JSON.parse(localStorage.getItem('records') || '[]');
  records.push(rec);
  localStorage.setItem('records', JSON.stringify(records));
}
function loadRecords() {
  let records = JSON.parse(localStorage.getItem('records') || '[]');
  recordList.innerHTML = '';
  records.slice(-10).reverse().forEach(r => {
    const li = document.createElement('li');
    li.textContent = `${r.date}｜${r.mode}｜${r.duration}分鐘｜${r.result}`;
    recordList.appendChild(li);
  });
}
loadRecords();

// PWA 安裝提示
if ('serviceWorker' in navigator) {
  navigator.serviceWorker.register('service-worker.js');
}

// 預留 API 介面
window.BreathFlowAPI = {
  onBioData: (data) => {}, // 生理數據
  onShare: (data) => {}, // 社群分享
  onExpertCourse: (data) => {} // 專家課程
};

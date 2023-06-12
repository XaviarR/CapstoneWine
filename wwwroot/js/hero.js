//import * as THREE from "three"
//import { GLTFLoader } from "three/addons/loaders/GLTFLoader.js";
//import { OrbitControls } from "three/addons/controls/OrbitControls.js";

//let view = document.getElementById("h_model")
//let scene = new THREE.Scene();
//let camera = new THREE.PerspectiveCamera(
//    75, view.clientWidth / view.clientHeight,
//    0.1,
//    1000
//    );

//    let renderer = new THREE.WebGL1Renderer({ alpha: true });
//    renderer.setSize(view.clientWidth, view.clientHeight);
//    view.appendChild(renderer.domElement);

//    //instance loader
//const loader = new GLTFLoader();
//loader.load('../assets/winez.gltf',function
//        (gltf) {
//            const model = gltf;
//            gltf.scene.scale.set(6, 6, 6);
//            const box = new THREE.Box3().setFromObject(gltf.scene);
//            const center = box.getCenter(new THREE.Vector3());
//            gltf.scene.position.x += gltf.scene.position.x - center.x;
//            gltf.scene.position.y += gltf.scene.position.y - center.y;
//            gltf.scene.position.z += gltf.scene.position.z - center.z;
//            scene.add(camera);
//            scene.add(gltf.scene);
//    },
//        (xhr) => {
//            console.log(
//                "Model" + (xhr.loaded / xhr.total) * 100 + "% loaded"
//            );
//        },
//        (error) => {
//            alert("Error loading model");
//            console.error(error);
//        },
//    undefined, function (error) {
//        console.error(error);
//    }
//    ); 


import { request } from 'https';
import * as THREE from 'three';
const scene = new THREE.Scene();
const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);

const renderer = new THREE.WebGLRenderer();
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(render.domElement);

const geometry = new Three.BoxGeomtry(1, 1, 1);
const material = new THREE.MeshBasicMaterial({ color: 0x00ff00 });
scene.add(cube);

camera.position.z = 5;

function animate() {
	requestAnimationFrame(animate);
	renderer.render(scene, camera);
	cube.rotation.x += 0.01;
	cube.rotation.y += 0.01;
}
animate();